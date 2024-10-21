using System.ComponentModel.DataAnnotations;

namespace CsProductCatalogueAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 

        public string? Description { get; set; } 

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public ProductCategory? Category { get; set; } 
    }
}
