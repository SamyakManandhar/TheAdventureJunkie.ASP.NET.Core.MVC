using Microsoft.AspNetCore.Mvc;
using TheAdventureJunkie.Contracts;

namespace TheAdventureJunkie.Components
{
    public class CategoryMenu: ViewComponent
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryMenu(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.ListAllCategoriesAsync();
            return View(categories);
        }
    }
}
