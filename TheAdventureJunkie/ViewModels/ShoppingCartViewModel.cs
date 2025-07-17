using TheAdventureJunkie.Contracts;

namespace TheAdventureJunkie.ViewModels
{
    public class ShoppingCartViewModel
	{
		public ShoppingCartViewModel(IShoppingCartService shoppingCart, decimal shoppingCartTotal)
		{
			ShoppingCart = shoppingCart;
			ShoppingCartTotal = shoppingCartTotal;
		}

		public IShoppingCartService ShoppingCart { get; }
		public decimal ShoppingCartTotal { get; }
	}
}
