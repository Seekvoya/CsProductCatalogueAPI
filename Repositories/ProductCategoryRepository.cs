using CsProductCatalogueAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CsProductCatalogueAPI.Data;

namespace CsProductCatalogueAPI.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryStatisticsDto>> GetCategoryStatisticsAsync(int page, int pageSize)
        {
            //Загружаем все категории и продукты в память, оговорюсь, что способ плох для работы с большими данными
            var productCategories = await _context.ProductCategories
                .Include(c => c.Products)
                .ToListAsync();

            //Выполняем вычисления в памяти с помощью LINQ to Objects
            var result = productCategories
                .Select(c => new CategoryStatisticsDto
                {
                    CategoryName = c.Name,
                    ProductCount = c.Products.Count(),
                    AveragePrice = c.Products.Any()
                        ? c.Products.Average(p => (decimal?)p.Price) ?? 0m
                        : 0m
                })
                .OrderBy(x => x.CategoryName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return result;
        }


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
            await _context.ProductCategories.AddAsync(category);
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




