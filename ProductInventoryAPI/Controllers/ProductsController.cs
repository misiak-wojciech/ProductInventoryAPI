using Microsoft.AspNetCore.Mvc;
using ProductInventoryAPI.DTOs;
using ProductInventoryAPI.Models;
using ProductInventoryAPI.Repositories;

namespace ProductInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
        {
            
            var category = await _categoryRepository.GetByIdAsync(productDTO.CategoryId);
            if (category == null)
            {
                return NotFound(new { message = "The category with the given ID does not exist." });
            }

            
            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                IsAvailable = productDTO.IsAvailable,
                CategoryId = productDTO.CategoryId
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            
            productDTO.Id = product.Id;

            return CreatedAtAction(nameof(GetProduct), new { id = productDTO.Id }, productDTO);
        }


        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts(
        int page = 1,
        int pageSize = 10,
        string sortBy = "name",
        string sortOrder = "asc"
        )
        {
            var products = await _productRepository.GetAllAsync();



            if (sortBy == "price")
            {
                products = sortOrder.ToLower() == "asc"
                    ? products.OrderBy(p => p.Price).ToList()
                    : products.OrderByDescending(p => p.Price).ToList();
            }
            else
            {
                products = sortOrder.ToLower() == "asc"
                    ? products.OrderBy(p => p.Name).ToList()
                    : products.OrderByDescending(p => p.Name).ToList();
            }


            var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (!paginatedProducts.Any())
            {
                return NotFound(new { message = "Brak produktów spełniających kryteria." });
            }


            var productDTOs = paginatedProducts.Select(product => new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                IsAvailable = product.IsAvailable,
                CategoryId = product.CategoryId
            }).ToList();

            return Ok(productDTOs);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                IsAvailable = product.IsAvailable,
                CategoryId = product.CategoryId
            };

            return Ok(productDTO);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {

            var category = await _categoryRepository.GetByIdAsync(productDTO.CategoryId);
            if (category == null)
            {
                return NotFound(new { message = "The category with the given ID does not exist." });
            }


            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }


            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.IsAvailable = productDTO.IsAvailable;
            product.CategoryId = productDTO.CategoryId;


            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return NoContent();
        }
       


        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();

            return NoContent(); 
        }



        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);

            if (!products.Any())
            {
                return NotFound(new { message = "No products found for the given category." });
            }

            return Ok(products);
        }


        // /api/products/search?query=query
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            var products = await _productRepository.SearchAsync(query);

            if (!products.Any())
            {
                return NotFound(new { message = "No products found matching the search criteria." });
            }

           
            var productDTOs = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                IsAvailable = p.IsAvailable,
                CategoryId = p.CategoryId
            });

            return Ok(productDTOs);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableProducts()
        {
            var products = await _productRepository.GetAvailableProductsAsync();

            if (!products.Any())
            {
                return NotFound(new { message = "No available products found." });
            }

            var productDTOs = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                IsAvailable = p.IsAvailable,
                CategoryId = p.CategoryId
            });

            return Ok(productDTOs);
        }




    }



}
