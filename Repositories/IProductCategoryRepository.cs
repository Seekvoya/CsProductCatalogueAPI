using CsProductCatalogueAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();
        Task<ProductCategory> GetCategoryByIdAsync(int id);
        Task<ProductCategory> AddCategoryAsync(ProductCategory category);
        Task<ProductCategory> UpdateCategoryAsync(ProductCategory category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}


