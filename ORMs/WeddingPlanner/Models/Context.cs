#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Models;

public class Context : DbContext
{
    public Context(DbContextOptions options)
        : base(options) { }

    public DbSet<Wedding> Weddings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Attendance> GuestLists { get; set; }
}
