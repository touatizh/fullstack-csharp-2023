#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models;

public class Order
{
    [Key]
    public int ID { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customer? Orderer { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product? OrderedProduct { get; set; }

    [Required]
    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
