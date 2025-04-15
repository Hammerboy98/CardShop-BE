using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardShop.Models; // Namespace dove si trova la tua classe Card
using CardShop.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cards (accessibile a tutti)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/cards/category/pokemon (accessibile a tutti - carte per categoria)
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardsByCategory(string category)
        {
            return await _context.Cards
                .Where(c => c.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }

        // GET: api/cards/5 (accessibile a tutti)
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // POST: api/cards (solo per admin, incluse le persone con username 'Ettore')
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            // Controlla se l'utente è Ettore o ha il ruolo admin
            if (User.Identity.Name != "Ettore" && !User.IsInRole("admin"))
            {
                return Unauthorized("Non hai i permessi per aggiungere una carta.");
            }

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // PUT: api/cards/5 (solo per admin, incluse le persone con username 'Ettore')
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            // Controlla se l'utente è Ettore o ha il ruolo admin
            if (User.Identity.Name != "Ettore" && !User.IsInRole("admin"))
            {
                return Unauthorized("Non hai i permessi per modificare una carta.");
            }

            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cards.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/cards/5 (solo per admin, incluse le persone con username 'Ettore')
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            // Controlla se l'utente è Ettore o ha il ruolo admin
            if (User.Identity.Name != "Ettore" && !User.IsInRole("admin"))
            {
                return Unauthorized("Non hai i permessi per eliminare una carta.");
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}


