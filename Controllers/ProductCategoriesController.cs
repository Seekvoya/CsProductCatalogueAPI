using CsProductCatalogueAPI.DTOs;
using CsProductCatalogueAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CsProductCatalogueAPI.Controllers
{
    [ApiController]
    [Route("api/product_categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoriesController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<IEnumerable<CategoryStatisticsDto>>> GetCategoryStatistics(
           int page = 1,
           int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be positive integers.");
            }

            var statistics = await _categoryService.GetCategoryStatisticsAsync(page, pageSize);
            return Ok(statistics);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> CreateCategory(ProductCategoryDto categoryDto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, ProductCategoryDto categoryDto)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}






