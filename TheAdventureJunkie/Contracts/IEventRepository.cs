using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface IEventRepository
    {
        IEnumerable<Event> AllEvents { get; }
        Event? GetEventById(int eventId);
    }
}
