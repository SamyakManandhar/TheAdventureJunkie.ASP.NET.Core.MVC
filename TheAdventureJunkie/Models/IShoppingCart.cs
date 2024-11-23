namespace TheAdventureJunkie.Models
{
	public interface IShoppingCart
	{
		void AddToCart(Event events);
		int RemoveFromCart(Event events);
		List<ShoppingCartItem> GetShoppingCartItems();
		void ClearCart();
		decimal GetShoppingCartTotal();
		List<ShoppingCartItem> ShoppingCartItems { get; set; }

	}
}
