
namespace TheAdventureJunkie.Models
{
	public class MockCategoryRepository : ICategoryRepository
	{
		public IEnumerable<Category> AllCategories => new List<Category>
			{
				new Category{CategoryId=1, CategoryName="Novice", Description="Starter"},
				new Category{CategoryId=2, CategoryName="Experienced", Description="Seasoned"},
				new Category{CategoryId=3, CategoryName="Sucidal", Description="Death Wish"}
			};
	}
}
