using TheAdventureJunkie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Validators;
using TheAdventureJunkie.Models.Dto;


namespace TheAdventureJunkie.Controllers
{
    [Authorize]

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartService _shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCartService shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(OrderDto orderdto)
        {
            OrderValidator validator = new OrderValidator();
            var validationResult = validator.Validate(orderdto);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }
                return View(orderdto);
            }
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty");
            }
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    FirstName = orderdto.FirstName,
                    LastName = orderdto.LastName,
                    AddressLine1 = orderdto.AddressLine1,
                    AddressLine2 = orderdto.AddressLine2,
                    ZipCode = orderdto.ZipCode,
                    City = orderdto.City,
                    State = orderdto.State,
                    Country = orderdto.Country,
                    PhoneNumber = orderdto.PhoneNumber,
                    Email = orderdto.Email,
                    OrderTotal = orderdto.OrderTotal,
                    OrderPlaced = orderdto.OrderPlaced
                };
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return View(orderdto);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your order. We will see you soon";
            return View();
        }
    }
}
