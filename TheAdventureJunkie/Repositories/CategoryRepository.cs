using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Category>> ListAllCategoriesAsync()
        {
            return await _theAdventureJunkieDbContext.Categories.ToListAsync();
        }
    }
}
