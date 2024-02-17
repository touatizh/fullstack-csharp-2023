#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models;

public class Wedding
{
    [Key]
    public int ID { get; set; }

    [Required]
    [Display(Name = "Wedder One")]
    public string WedderOne { get; set; }

    [Required]
    [Display(Name = "Wedder Two")]
    public string WedderTwo { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation
    public List<Attendance> Guests { get; set; } = new List<Attendance>();
}
