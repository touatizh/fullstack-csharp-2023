#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class Product
{
    [Key]
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Image (URL)")]
    public string? ImageUrl { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Quatity value.")]
    public int Quatity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation
    public List<Order> OrderedBy { get; set; } = new List<Order>();
}
