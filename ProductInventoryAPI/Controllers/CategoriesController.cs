using Microsoft.AspNetCore.Mvc;
using ProductInventoryAPI.DTOs;
using ProductInventoryAPI.Models;
using ProductInventoryAPI.Repositories;

namespace ProductInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories(
        int page = 1,
        int pageSize = 2,
        string sortBy = "name", 
        string sortOrder = "asc" 
    )
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (sortBy == "name")
            {
                categories = sortOrder.ToLower() == "asc"
                    ? categories.OrderBy(c => c.Name).ToList()
                    : categories.OrderByDescending(c => c.Name).ToList();
            }

           
            var paginatedCategories = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (!paginatedCategories.Any())
            {
                return NotFound(new { message = "No categories match the given criteria." });
            }

          
            var categoryDTOs = paginatedCategories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return Ok(categoryDTOs);
        }


        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDTO);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            categoryDTO.Id = category.Id;

            return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.Id }, categoryDTO);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDTO.Name;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("with-product-count")]
        public async Task<IActionResult> GetCategoriesWithProductCount()
        {
            var categories = await _categoryRepository.GetCategoriesWithProductCountAsync();

            var result = categories.Select(c => new
            {
                c.Id,
                c.Name,
                ProductCount = c.Products.Count
            });

            return Ok(result);
        }


    }
}
