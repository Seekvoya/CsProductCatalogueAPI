using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CsProductCatalogueAPI.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

