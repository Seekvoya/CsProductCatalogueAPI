using CsProductCatalogueAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CsProductCatalogueAPI.Data;

namespace CsProductCatalogueAPI.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository // Исправлено имя класса
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context) // Исправлено имя класса
        {
            _context = context;
        }

        // Реализация методов интерфейса
        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task<ProductCategory> AddCategoryAsync(ProductCategory category)
        {
            await _context.ProductCategories.AddAsync(category); // Исправлено имя DbSet
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<ProductCategory> UpdateCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);
            if (category != null)
            {
                _context.ProductCategories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}




