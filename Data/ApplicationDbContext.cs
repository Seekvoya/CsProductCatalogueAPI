using Microsoft.EntityFrameworkCore;
using CsProductCatalogueAPI.Models; 

namespace CsProductCatalogueAPI.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

