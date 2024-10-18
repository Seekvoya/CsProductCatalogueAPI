using CsProductCatalogueAPI.Models;
using CsProductCatalogueAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();
        Task<ProductCategory> GetCategoryByIdAsync(int id);
        Task<ProductCategory> AddCategoryAsync(ProductCategory category);
        Task<ProductCategory> UpdateCategoryAsync(ProductCategory category);
        Task<bool> DeleteCategoryAsync(int id);
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categoryRepository;

        public ProductCategoryService(IProductCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<ProductCategory> AddCategoryAsync(ProductCategory category)
        {
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<ProductCategory> UpdateCategoryAsync(ProductCategory category)
        {
            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}


