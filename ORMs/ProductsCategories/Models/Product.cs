#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models;

public class Product
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association>? Categories { get; set; } = new List<Association>();
}