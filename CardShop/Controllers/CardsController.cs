using CardShop.Services;
using CardShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        // POST: api/card
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Card>> AddCard([FromBody] Card card)
        {
            var createdCard = await _cardService.AddCardAsync(card);
            return CreatedAtAction(nameof(GetCardById), new { id = createdCard.Id }, createdCard);
        }

        // GET: api/card
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetAllCards()
        {
            var cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }

        // GET: api/card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCardById(int id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        // GET: api/card/category/{category}
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardsByCategory(string category)
        {
            var cards = await _cardService.GetCardsByCategoryAsync(category);
            if (cards == null || !cards.Any())
            {
                return NotFound("Nessuna carta trovata per questa categoria.");
            }
            return Ok(cards);
        }

        // PUT: api/card/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Card>> UpdateCard(int id, [FromBody] Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            var updatedCard = await _cardService.UpdateCardAsync(card);
            return Ok(updatedCard);
        }

        // DELETE: api/card/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            var result = await _cardService.DeleteCardAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}




