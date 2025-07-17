using TheAdventureJunkie.Contracts;
using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TheAdventureJunkieDbContext _theAdventureJunkieDbContext;

        public CategoryRepository(TheAdventureJunkieDbContext theAdventureJunkieDbContext)
        {
            _theAdventureJunkieDbContext = theAdventureJunkieDbContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _theAdventureJunkieDbContext.Categories;
            }
        }
    }
}
