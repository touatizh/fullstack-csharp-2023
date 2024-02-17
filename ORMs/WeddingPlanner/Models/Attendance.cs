#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models;

public class Attendance
{
    [Key]
    public int ID { get; set; }

    // Guest
    [Required]
    public int GuestId { get; set; }
    public User? Guest { get; set; }

    // Wedding
    [Required]
    public int WeddingId { get; set; }
    public Wedding? Wedding { get; set; }
}
