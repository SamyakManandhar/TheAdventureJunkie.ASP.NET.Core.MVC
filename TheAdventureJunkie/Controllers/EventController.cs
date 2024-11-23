using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;
using TheAdventureJunkie.Models;
using TheAdventureJunkie.ViewModels;

namespace TheAdventureJunkie.Controllers
{
	public class EventController:Controller
	{
		private readonly IEventRepository _eventRepository;
		private readonly ICategoryRepository _categoryRepository;

		public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository)
		{
			_eventRepository = eventRepository;
			_categoryRepository = categoryRepository;
		}
		public IActionResult List(string category)
		{
            IEnumerable<Event> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _eventRepository.AllEvents.OrderBy(p => p.EventId);
                currentCategory = "All Events";
            }
            else
            {
                pies = _eventRepository.AllEvents.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.EventId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new EventListViewModel(pies, currentCategory));
        }

		public IActionResult Details(int id)
		{
			var eventObj = _eventRepository.GetEventById(id);
			if(eventObj == null)
			{
				return NotFound();
			}
			return View(eventObj);
		}
	}
}
