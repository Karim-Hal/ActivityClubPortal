using Microsoft.AspNetCore.Mvc;
using SampleOneWebAPI.Models;
using SampleOneWebAPI.Services.Interfaces;

namespace SampleOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            
        }
        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult> AddCategory(Category categoryModel)
        {
            try
            {
                if (categoryModel.Id == 0)
                {
                    await _categoryService.AddCategory(categoryModel);
                }
                else
                {
                    var checkIfExists = await _categoryService.CheckCategoryExists(categoryModel.Id);
                    if (checkIfExists)
                    {
                        await _categoryService.EditCategory(categoryModel);
                    }
                    else
                    {
                        return NotFound("Category not found");
                    }
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                if (await _categoryService.CheckCategoryExists(id) is false)
                {
                    return NotFound($"Category with id {id} not found!");
                }
                var categoryModel = await _categoryService.GetCategory(id);
                return Ok(categoryModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                if (await _categoryService.CheckCategoryExists(id))
                {
                    await _categoryService.DeleteCategory(id);
                    return Ok("Category Deleted Successfully!");
                }
                else
                {
                    return NotFound($"Category with id {id} does not exist");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
