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

        public async Task<ViewResult> Index()
        {
            var items = await _shoppingCart.ListShoppingCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, await _shoppingCart.GetShoppingCartTotalAsync());

            return View(shoppingCartViewModel);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int eventId)
        {
            var selectedEvent = await _eventRepository.GetEventByIdAsync(eventId);

            if (selectedEvent != null)
            {
                await _shoppingCart.AddToCartAsync(selectedEvent);
            }
            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int eventId)
        {
            var selectedEvent = await _eventRepository.GetEventByIdAsync(eventId);
            if (selectedEvent != null)
            {
                await _shoppingCart.RemoveFromCartAsync(selectedEvent);
            }
            return RedirectToAction("Index");
        }
    }
}