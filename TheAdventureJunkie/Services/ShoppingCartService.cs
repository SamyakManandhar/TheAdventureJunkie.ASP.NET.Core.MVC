
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;
        private ShoppingCartService(TheAdventureJunkieDbContext theAdventureJunkieDbContext)
        {
            _theAdventureJunkieDbContext = theAdventureJunkieDbContext;
        }

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            TheAdventureJunkieDbContext context = services.GetService<TheAdventureJunkieDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCartService(context) { ShoppingCartId = cartId };
        }

        public async Task AddToCartAsync(Event events)
        {
            var shoppingCartItem = await _theAdventureJunkieDbContext.ShoppingCartItems
             .SingleOrDefaultAsync(s => s.Event.EventId == events.EventId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Event = events,
                    Amount = 1
                };

                await _theAdventureJunkieDbContext.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            await _theAdventureJunkieDbContext.SaveChangesAsync();
        }

        public async Task ClearCartAsync()
        {
            var cartItems = await _theAdventureJunkieDbContext
                                  .ShoppingCartItems
                                  .Where(cart => cart.ShoppingCartId == ShoppingCartId)
                                  .ToListAsync();
            _theAdventureJunkieDbContext.ShoppingCartItems.RemoveRange(cartItems);
            await _theAdventureJunkieDbContext.SaveChangesAsync();
        }

        public async Task<List<ShoppingCartItem>> ListShoppingCartItemsAsync()
        {
            return ShoppingCartItems ??= await
                       _theAdventureJunkieDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(p => p.Event).ToListAsync();
        }

        public async Task<decimal> GetShoppingCartTotalAsync()
        {
            var total = await _theAdventureJunkieDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Event.Price * c.Amount)
                .SumAsync();
            return total;
        }

        public async Task<int> RemoveFromCartAsync(Event events)
        {
            var shoppingCartItem = await
                               _theAdventureJunkieDbContext.ShoppingCartItems.SingleOrDefaultAsync(
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
            await _theAdventureJunkieDbContext.SaveChangesAsync();
            return localAmount;
        }
    }
}
