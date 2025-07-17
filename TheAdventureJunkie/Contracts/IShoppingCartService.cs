using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface IShoppingCartService
    {
        void AddToCart(Event events);
        int RemoveFromCart(Event events);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
