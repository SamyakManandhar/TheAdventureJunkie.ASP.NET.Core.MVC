using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(Event events);
        Task<int> RemoveFromCartAsync(Event events);
        Task<List<ShoppingCartItem>> ListShoppingCartItemsAsync();
        Task ClearCartAsync();
        Task<decimal> GetShoppingCartTotalAsync();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
