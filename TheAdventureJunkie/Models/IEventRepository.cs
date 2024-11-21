namespace TheAdventureJunkie.Models
{
	public interface IEventRepository
	{
		IEnumerable<Event> AllEvents { get; }
		Event? GetEventById(int eventId);
	}
}
