using CsProductCatalogueAPI.DTOs;
using CsProductCatalogueAPI.Models;
using CsProductCatalogueAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsProductCatalogueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto productDto)
        {
            if (productDto.CategoryId > 0 &&
                !await _productRepository.CategoryExistsAsync(productDto.CategoryId))
            {
                return BadRequest($"Category with ID {productDto.CategoryId} does not exist.");
            }

            var product = ConvertToModel(productDto);
            await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, ConvertToDto(product));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDtos = products.Select(ConvertToDto).ToList();
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) 
                return NotFound();
                
            return Ok(ConvertToDto(product));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound($"Product with ID {id} does not exist.");

          if (productDto.CategoryId > 0 && !await _productRepository.CategoryExistsAsync(productDto.CategoryId))
                return BadRequest($"Category with ID {productDto.CategoryId} does not exist.");

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description; 
            existingProduct.Price = productDto.Price;
            existingProduct.CategoryId = productDto.CategoryId;

            await _productRepository.UpdateProductAsync(existingProduct);
            return Ok(ConvertToDto(existingProduct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null) return NotFound($"Product with ID {id} does not exist.");

            await _productRepository.DeleteProductAsync(id);
            
            // Возврат статуса 204 No Content после успешного удаления
            return NoContent();
        }

        private Product ConvertToModel(ProductDto dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
        }

        private ProductDto ConvertToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }
    }
}


