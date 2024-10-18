using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    public int CategoryId { get; set; }
    public ProductCategory Category { get; set; }
}
