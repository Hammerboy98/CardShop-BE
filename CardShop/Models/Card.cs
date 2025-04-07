namespace CardShop.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Expansion { get; set; }
        public string Rarity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
