#pragma warning disable CS8618
namespace FormSubmission.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    [MinLength(4, ErrorMessage = "First name must be at least 4 characters")]
    [Display(Name = "First Name:")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "Last name must be at least 4 characters")]
    [Display(Name = "Last Name:")]
    public string LastName { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Age must be a positive number")]
    [Display(Name = "Age:")]
    public int Age { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email:")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password:")]
    public string Password { get; set; }
}