using SampleOneWebAPI.Models;
using SampleOneWebAPI.Repositories.Interfaces;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository<Category> _repository;
        public CategoryService(ICategoryRepository categoryRepository, IRepository<Category> repository)
        {
            _categoryRepository = categoryRepository;
            _repository = repository;
            
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _repository.GetAll();
            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _repository.GetById(id);
            return category;
        }

        public async Task AddCategory(Category category)
        {
            await _repository.Add(category);
        }

        public async Task<bool> CheckCategoryExists(int id)
        {
            return await _repository.CheckEntityExists(id);
        }

        public async Task EditCategory(Category category)
        {
            await _repository.Update(category);
        }

        public async Task DeleteCategory(int id)
        {
            await _repository.Delete(id);
        }

    }
}
