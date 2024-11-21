using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.ViewModels
{
	public class EventListViewModel
	{
		public IEnumerable<Event> Events { get; }
		public string? CurrentCategory { get; }

		public EventListViewModel(IEnumerable<Event> events, string? currentCategory)
		{
			Events = events;
			CurrentCategory = currentCategory;
		}
	}
}
