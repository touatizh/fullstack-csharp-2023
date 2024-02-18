#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class Customer
{
    [Key]
    public int ID { get; set; }

    [Required]
    [Display(Name = "Customer Name")]
    public string Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation
    public List<Order> OrderedProducts { get; set; } = new List<Order>();
}
