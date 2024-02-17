#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class User
{
    [Key]
    public int ID { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [NotMapped]
    [Compare("Password")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string ConfirmPwd { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Navigation
    public List<Attendance> Attending { get; set; } = new List<Attendance>();
}
