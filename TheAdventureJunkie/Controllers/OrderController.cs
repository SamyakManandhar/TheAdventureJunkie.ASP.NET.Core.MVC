using TheAdventureJunkie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace TheAdventureJunkie.Controllers
{
	[Authorize]

	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IShoppingCart _shoppingCart;

		public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
		{
			_orderRepository = orderRepository;
			_shoppingCart = shoppingCart;
		}
		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order) 
		{ 
			var items=_shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			if(_shoppingCart.ShoppingCartItems.Count == 0)
			{
				ModelState.AddModelError("", "Your cart is empty");
			}
			if (ModelState.IsValid)
			{
				_orderRepository.CreateOrder(order);
				_shoppingCart.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}
			return View(order);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thank you for your order. We will see you soon";
			return View();
		}
	}
}
