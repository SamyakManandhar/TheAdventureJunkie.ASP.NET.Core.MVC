
using Microsoft.EntityFrameworkCore;

namespace TheAdventureJunkie.Models
{
	public class EventRepository : IEventRepository
	{
		private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;

		public EventRepository(TheAdventureJunkieDbContext theAdventureJunkieDbContext)
		{
			_theAdventureJunkieDbContext = theAdventureJunkieDbContext;
		}

		public IEnumerable<Event> AllEvents
		{
			get
			{
				return _theAdventureJunkieDbContext.Events.Include(e => e.Category);
			}
		}
		public Event? GetEventById(int eventId)
		{
			return _theAdventureJunkieDbContext.Events.FirstOrDefault(e => e.EventId == eventId);
		}
	}
}
