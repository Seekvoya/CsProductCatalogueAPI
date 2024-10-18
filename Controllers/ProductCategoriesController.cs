using CsProductCatalogueAPI.DTOs;
using CsProductCatalogueAPI.Models;
using CsProductCatalogueAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories); // Возможно, вам нужно будет преобразовать к DTO
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(ConvertToDto(category)); // Добавить метод ConvertToDto
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


