using Microsoft.EntityFrameworkCore;
using TraineesAPI.Models;

namespace TraineesAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Trainee> Trainees => Set<Trainee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Trainee → Track (many-to-one, optional)
        modelBuilder.Entity<Trainee>()
            .HasOne(t => t.Track)
            .WithMany(tr => tr.Trainees)
            .HasForeignKey(t => t.TrackId)
            .OnDelete(DeleteBehavior.SetNull);

        // Store Gender enum as a string for readability
        modelBuilder.Entity<Trainee>()
            .Property(t => t.Gender)
            .HasConversion<string>();
    }
}
