using Microsoft.AspNetCore.Mvc;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.ViewModels;

namespace TheAdventureJunkie.Controllers
{
    public class ShoppingCartController : Controller
	{
		private readonly IEventRepository _eventRepository;
		private readonly IShoppingCartService _shoppingCart;

		public ShoppingCartController(IEventRepository eventRepository, IShoppingCartService shoppingCart)
		{
			_eventRepository = eventRepository;
			_shoppingCart = shoppingCart;
		}

		public ViewResult Index()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());

			return View(shoppingCartViewModel);
		}

		public RedirectToActionResult AddToShoppingCart(int eventId)
		{
			var selectedEvent = _eventRepository.AllEvents.FirstOrDefault(p => p.EventId == eventId);

			if (selectedEvent != null)
			{
				_shoppingCart.AddToCart(selectedEvent);
			}
			return RedirectToAction("Index");
		}

		public RedirectToActionResult RemoveFromShoppingCart(int eventId)
		{
			var selectedEvent = _eventRepository.AllEvents.FirstOrDefault(p => p.EventId == eventId);

			if (selectedEvent != null)
			{
				_shoppingCart.RemoveFromCart(selectedEvent);
			}
			return RedirectToAction("Index");
		}
	}
}