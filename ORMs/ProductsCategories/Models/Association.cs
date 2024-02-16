#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models;

public class Association
{
    [Key]
    public int ID { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

}