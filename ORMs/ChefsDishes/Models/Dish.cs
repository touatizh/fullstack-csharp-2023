#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefsDishes.Models;

public class Dish
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Dish name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Chef's name is required")]
    public int ChefId { get; set; }

    [Required(ErrorMessage = "Tastiness is required")]
    [Range(1, 5, ErrorMessage = "Tastiness must be between 1 and 5")]
    public int Tastiness { get; set; }

    [Required(ErrorMessage = "Calories is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0")]
    public int Calories { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Chef? Chef { get; set; }
}