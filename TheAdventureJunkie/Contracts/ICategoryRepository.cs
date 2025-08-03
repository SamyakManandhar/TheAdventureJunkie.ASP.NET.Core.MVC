using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAllCategoriesAsync();
    }
}
