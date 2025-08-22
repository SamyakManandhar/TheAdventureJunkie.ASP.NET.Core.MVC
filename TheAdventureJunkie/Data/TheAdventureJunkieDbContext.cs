using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Data
{
    public class TheAdventureJunkieDbContext : IdentityDbContext
    {
        public TheAdventureJunkieDbContext(DbContextOptions<TheAdventureJunkieDbContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
