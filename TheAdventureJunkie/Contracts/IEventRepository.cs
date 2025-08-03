using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> ListAllEventsAsync();
        Task<Event?> GetEventByIdAsync(int eventId);
    }
}
