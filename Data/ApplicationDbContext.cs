using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CS330_Fall2024_FinalProject.Data;

public class ApplicationDbContext : IdentityDbContext<Athlete>
{

    public DbSet<SkiStats> SkiStats { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Athlete>().HasData(
        //     new Athlete { Id = 1, Name = "John Doe", Number = 7, SkiLevel = "Intermediate", Birthday = new DateTime(1995, 4, 23) },
        //     new Athlete { Id = 2, Name = "Jane Smith", Number = 15, SkiLevel = "Beginner", Birthday = new DateTime(1998, 7, 14) }
        // );

        // modelBuilder.Entity<SkiStats>().HasData(
        //     new SkiStats { Id = 1, BestTime = 45.6, TopSpeed = 120.0, BestDistance = 3000.0, VerticalDrop = 800.0, Ranking = 1.2 },
        //     new SkiStats { Id = 2, BestTime = 50.0, TopSpeed = 110.0, BestDistance = 2800.0, VerticalDrop = 700.0, Ranking = 2.5 }
        // );

        modelBuilder.Entity<Schedule>().HasData(
            new Schedule { Id = 1, Type = "Game", Date = new DateTime(2024, 12, 10, 15, 0, 0), Description = "Season Opener", Location = "Mountain Arena" },
            new Schedule { Id = 2, Type = "Practice", Date = new DateTime(2024, 12, 8, 10, 0, 0), Description = "Morning Practice", Location = "Training Grounds" },
            new Schedule { Id = 3, Type = "Event", Date = new DateTime(2024, 12, 12, 18, 0, 0), Description = "Charity Ski Gala", Location = "Snow Peaks Resort" }
        );

    }
}
