using System.Collections.Generic;
using System.Threading.Tasks;
using CsProductCatalogueAPI.Models;

namespace CsProductCatalogueAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> CategoryExistsAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}

