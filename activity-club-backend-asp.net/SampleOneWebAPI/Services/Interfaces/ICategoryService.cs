using SampleOneWebAPI.Models;

namespace SampleOneWebAPI.Services.Interfaces
{
    public interface ICategoryService
    { 
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task AddCategory(Category category);
        Task<bool> CheckCategoryExists(int id);
        Task EditCategory(Category category);
        Task DeleteCategory(int id);
    }
}
