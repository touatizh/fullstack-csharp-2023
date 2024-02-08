#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models;

public class LoginUser
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string LoginEmail { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }

}