using Microsoft.EntityFrameworkCore;

namespace TheAdventureJunkie.Models
{
	public class OrderRepository : IOrderRepository
	{
		private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;
		private readonly IShoppingCart _shoppingCart;

		public OrderRepository(TheAdventureJunkieDbContext theAdventureJunkieDbContext, IShoppingCart shoppingCart)
		{
			_theAdventureJunkieDbContext = theAdventureJunkieDbContext;
			_shoppingCart = shoppingCart;
		}

		public void CreateOrder(Order order)
		{
			order.OrderPlaced = DateTime.Now;

			List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
			order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

			order.OrderDetails = new List<OrderDetail>();

			foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
			{
				var orderDetail = new OrderDetail
				{
					Amount = shoppingCartItem.Amount,
					PieId = shoppingCartItem.Event.EventId,
					Price = shoppingCartItem.Event.Price
				};

				order.OrderDetails.Add(orderDetail);
			}

			_theAdventureJunkieDbContext.Orders.Add(order);

			_theAdventureJunkieDbContext.SaveChanges();
		}
	}
}
