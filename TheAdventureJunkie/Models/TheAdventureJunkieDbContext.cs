using Microsoft.EntityFrameworkCore;

namespace TheAdventureJunkie.Models
{
	public class TheAdventureJunkieDbContext : DbContext
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
