using Microsoft.EntityFrameworkCore;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;
        private readonly IShoppingCartService _shoppingCart;

        public OrderRepository(TheAdventureJunkieDbContext theAdventureJunkieDbContext, IShoppingCartService shoppingCart)
        {
            _theAdventureJunkieDbContext = theAdventureJunkieDbContext;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = await _shoppingCart.GetShoppingCartTotalAsync();
            order.OrderDetails = new List<OrderDetail>();
            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    EventId = shoppingCartItem.Event.EventId,
                    Price = shoppingCartItem.Event.Price
                };

                order.OrderDetails.Add(orderDetail);
            }
            await _theAdventureJunkieDbContext.Orders.AddAsync(order);
            await _theAdventureJunkieDbContext.SaveChangesAsync();
        }
    }
}
