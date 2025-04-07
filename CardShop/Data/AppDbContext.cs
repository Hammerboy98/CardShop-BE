using Microsoft.EntityFrameworkCore;
using CardShop.Models;

namespace CardShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
