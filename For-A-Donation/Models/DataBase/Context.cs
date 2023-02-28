using Microsoft.EntityFrameworkCore;

namespace For_A_Donation.Models.DataBase;

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
}
