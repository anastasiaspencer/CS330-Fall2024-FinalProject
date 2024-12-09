using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
namespace CS330_Fall2024_FinalProject.Models;

public class UserRole : IdentityRole{
    
}
public class Athlete : IdentityUser
{

   // public int Id { get; set; }  // This is the primary key
    public string Name  { get; set; } = "Unknown";
    public int Number { get; set; }

    // public string SkiLevel { get; set; }
    public string? SkiLevel { get; set; } = null;

    //Add fields for other data we want stored in each athlete model
    // public SkiStats Stats { get; set; }

    public SkiStats? Stats { get; set; } = null;
    public DateTime Birthday { get; set; } 

    // public byte[] ProfilePicture { get; set; }
    public byte[]? ProfilePicture { get; set; } = null;

    public Athlete() { } //paramaterless constructor that EF Core Expects
    public Athlete(int number, string name, SkiStats stats, string? skilevel, DateTime birthday, byte[]? profilePicture) //, byte[] profilePicture
    {
        Number = number;
        Name = name;
        Stats = stats;
        SkiLevel = skilevel ?? "Unknown";
        Birthday = birthday;
        ProfilePicture = profilePicture ?? Array.Empty<byte>();
    }
}






