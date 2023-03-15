using Microsoft.EntityFrameworkCore;
using For_A_Donation.Domain.Core.Models;
using Task = For_A_Donation.Domain.Core.Models.Task;

namespace For_A_Donation.Infrastructure.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }

    public DbSet<Family> Families { get; set; }

    public DbSet<Progress> Progress { get; set; }

    public DbSet<UserProgress> UserProgress { get; set; }

    public DbSet<Reward> Rewards { get; set; }

    public DbSet<Task> Tasks { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Purpose> Purposes { get; set; }

    public DbSet<Wish> Wishes { get; set; }
}
