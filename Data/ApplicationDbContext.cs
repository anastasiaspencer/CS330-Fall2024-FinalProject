﻿using CS330_Fall2024_FinalProject.Models;
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
}

