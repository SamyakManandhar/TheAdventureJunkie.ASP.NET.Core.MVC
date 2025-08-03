using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Models;
using TheAdventureJunkie.ViewModels;

namespace TheAdventureJunkie.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;

        public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> List(string category)
        {
            IEnumerable<Event> events;
            string? currentCategory;
            var allEvents = await _eventRepository.ListAllEventsAsync();

            if (string.IsNullOrEmpty(category))
            {
                events = allEvents.OrderBy(p => p.EventId);
                currentCategory = "All Events";
            }
            else
            {
                events = allEvents.Where(p => p.Category.CategoryName == category)
                                  .OrderBy(p => p.EventId);
                var allCategories = await _categoryRepository.ListAllCategoriesAsync();
                currentCategory = allCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new EventListViewModel(events, currentCategory));
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventObj = await _eventRepository.GetEventByIdAsync(id);
            if (eventObj == null)
            {
                return NotFound();
            }
            return View(eventObj);
        }
    }
}
