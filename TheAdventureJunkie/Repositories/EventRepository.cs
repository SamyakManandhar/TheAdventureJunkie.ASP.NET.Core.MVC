
using Microsoft.EntityFrameworkCore;
using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;

        public EventRepository(TheAdventureJunkieDbContext theAdventureJunkieDbContext)
        {
            _theAdventureJunkieDbContext = theAdventureJunkieDbContext;
        }

        public async Task<IEnumerable<Event>> ListAllEventsAsync()
        {
            return await _theAdventureJunkieDbContext.Events.Include(e => e.Category).ToListAsync();
        }
        public async Task<Event?> GetEventByIdAsync(int eventId)
        {
            return await _theAdventureJunkieDbContext.Events.FirstOrDefaultAsync(e => e.EventId == eventId);
        }
    }
}
