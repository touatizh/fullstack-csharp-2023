#pragma warning disable CS8618
namespace DojoSurvey.Models;
using System.ComponentModel.DataAnnotations;

public class SurveyForm
{
    [Required]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string name { get; set; }

    [Required]
    public string location { get; set; }

    [Required]
    public string language { get; set; }

    [MaxLength(20, ErrorMessage = "Comment  must not exceed 20 characters")]
    public string? comments { get; set; }
}