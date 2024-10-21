using CsProductCatalogueAPI.DTOs;
using CsProductCatalogueAPI.Models;
using CsProductCatalogueAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryService(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoryDtos = new List<ProductCategoryDto>();
            foreach (var category in categories)
            {
                categoryDtos.Add(new ProductCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
            }
            return categoryDtos;
        }

        public async Task<ProductCategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return null;
            return new ProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<ProductCategoryDto> CreateCategoryAsync(ProductCategoryDto categoryDto)
        {
            var category = new ProductCategory
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            var createdCategory = await _categoryRepository.AddCategoryAsync(category);
            return new ProductCategoryDto
            {
                Id = createdCategory.Id,
                Name = createdCategory.Name,
                Description = createdCategory.Description
            };
        }

        public async Task<ProductCategoryDto> UpdateCategoryAsync(int id, ProductCategoryDto categoryDto)
        {
            var category = new ProductCategory
            {
                Id = id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);
            return new ProductCategoryDto
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}




