
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace TheAdventureJunkie.Models
{
	public class ShoppingCart : IShoppingCart
	{
		private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;
		private ShoppingCart(TheAdventureJunkieDbContext theAdventureJunkieDbContext)
		{
			_theAdventureJunkieDbContext = theAdventureJunkieDbContext;
		}

		public string? ShoppingCartId { get; set; }

		public List<ShoppingCartItem> ShoppingCartItems { get; set; }=default!;

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

			TheAdventureJunkieDbContext context = services.GetService<TheAdventureJunkieDbContext>() ?? throw new Exception("Error initializing");

			string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

			session?.SetString("CartId", cartId);

			return new ShoppingCart(context) { ShoppingCartId = cartId };
		}

		public void AddToCart(Event events)
		{
			var shoppingCartItem =
					_theAdventureJunkieDbContext.ShoppingCartItems.SingleOrDefault(
						s => s.Event.EventId == events.EventId && s.ShoppingCartId == ShoppingCartId);

			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = ShoppingCartId,
					Event = events,
					Amount = 1
				};

				_theAdventureJunkieDbContext.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}
			_theAdventureJunkieDbContext.SaveChanges();
		}

		public void ClearCart()
		{
			var cartItems = _theAdventureJunkieDbContext
							.ShoppingCartItems
							.Where(cart => cart.ShoppingCartId == ShoppingCartId);

			_theAdventureJunkieDbContext.ShoppingCartItems.RemoveRange(cartItems);

			_theAdventureJunkieDbContext.SaveChanges();
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??=
					   _theAdventureJunkieDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
					   .Include(p => p.Event).ToList();
		}

		public decimal GetShoppingCartTotal()
		{
			var total = _theAdventureJunkieDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
			   .Select(c => c.Event.Price * c.Amount).Sum();
			return total;
		}

		public int RemoveFromCart(Event events)
		{
			var shoppingCartItem =
							   _theAdventureJunkieDbContext.ShoppingCartItems.SingleOrDefault(
								   s => s.Event.EventId == events.EventId && s.ShoppingCartId == ShoppingCartId);

			var localAmount = 0;

			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_theAdventureJunkieDbContext.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_theAdventureJunkieDbContext.SaveChanges();

			return localAmount;
		}
	}
}
