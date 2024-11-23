namespace TheAdventureJunkie.Models
{
	public class ShoppingCartItem
	{
		public int ShoppingCartItemId { get; set; }
		public Event Event { get; set; } = default!;
		public int Amount { get; set; }
		public string? ShoppingCartId { get; set; }
	}
}
