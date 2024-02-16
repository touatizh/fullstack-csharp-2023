#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models;

public class Category
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association>? Products { get; set; } = new List<Association>();
}