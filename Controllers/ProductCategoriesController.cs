using CsProductCatalogueAPI.DTOs;
using CsProductCatalogueAPI.Models;
using CsProductCatalogueAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Controllers
{
    [ApiController]
    [Route("api/product_categories")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoriesController(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(ProductCategoryDto categoryDto)
        {
            var category = ConvertToModel(categoryDto);
            await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, ConvertToDto(category));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoryDtos = categories.Select(ConvertToDto);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(ConvertToDto(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, ProductCategoryDto categoryDto)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null) return NotFound($"Category with ID {id} does not exist.");

            existingCategory.Name = categoryDto.Name;

            await _categoryRepository.UpdateCategoryAsync(existingCategory);
            return Ok(ConvertToDto(existingCategory)); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null) return NotFound($"Category with ID {id} does not exist.");

            await _categoryRepository.DeleteCategoryAsync(id);
            return NoContent(); // Возврат статуса 204 No Content после успешного удаления
        }

        private ProductCategory ConvertToModel(ProductCategoryDto dto)
        {
            return new ProductCategory
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        private ProductCategoryDto ConvertToDto(ProductCategory category)
        {
            return new ProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
    }
}



