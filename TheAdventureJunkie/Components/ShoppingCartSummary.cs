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

		public IViewComponentResult Invoke()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

			return View(shoppingCartViewModel);
		}
	}
}
