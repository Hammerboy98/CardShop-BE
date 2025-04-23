using CardShop.Data;
using CardShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Services
{
    public class CardService
    {
        private readonly AppDbContext _context;

        public CardService(AppDbContext context)
        {
            _context = context;
        }

        // Aggiungi una nuova carta
        public async Task<Card> AddCardAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        // Ottieni tutte le carte
        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        // Ottieni una carta per ID
        public async Task<Card> GetCardByIdAsync(int id)
        {
            return await _context.Cards.FindAsync(id);
        }

        // Aggiorna una carta
        public async Task<Card> UpdateCardAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        // Rimuovi una carta
        public async Task<bool> DeleteCardAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) return false;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        // Ottieni carte per categoria
        public async Task<List<Card>> GetCardsByCategoryAsync(string category)
        {
            return await _context.Cards
                                 .Where(c => c.Category == category)
                                 .ToListAsync();
        }
    }
}


