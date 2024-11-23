using Microsoft.AspNetCore.Mvc;
using TheAdventureJunkie.Models;

namespace TheAdventureJunkie.Components
{
	public class CategoryMenu: ViewComponent
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryMenu(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IViewComponentResult Invoke()
		{
			var categories = _categoryRepository.AllCategories;
			return View(categories);
		}
	}
}
