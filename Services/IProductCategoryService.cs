using System.Collections.Generic;
using System.Threading.Tasks;
using CsProductCatalogueAPI.DTOs;

namespace CsProductCatalogueAPI.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllCategoriesAsync();
        Task<ProductCategoryDto> GetCategoryByIdAsync(int id);
        Task<ProductCategoryDto> CreateCategoryAsync(ProductCategoryDto categoryDto);
        Task<ProductCategoryDto> UpdateCategoryAsync(int id, ProductCategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
