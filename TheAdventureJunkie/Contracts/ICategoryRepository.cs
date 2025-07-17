using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }

    }
}
