using Microsoft.AspNetCore.Mvc;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.ViewModels;

namespace TheAdventureJunkie.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartSummary(IShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _shoppingCart.ListShoppingCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, await _shoppingCart.GetShoppingCartTotalAsync());

            return View(shoppingCartViewModel);
        }
    }
}
