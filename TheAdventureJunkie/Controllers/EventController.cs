using Microsoft.AspNetCore.Mvc;
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
		public IActionResult List()
		{
			EventListViewModel eventListViewModel = new EventListViewModel(_eventRepository.AllEvents, "All Events");
			return View(eventListViewModel);
		}
	}
}
