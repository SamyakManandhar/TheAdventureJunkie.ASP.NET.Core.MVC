namespace TheAdventureJunkie.Models
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> AllCategories { get; }

	}
}
