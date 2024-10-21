using CsProductCatalogueAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto); // Добавлено
        Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}

