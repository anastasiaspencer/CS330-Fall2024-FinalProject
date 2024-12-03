using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
namespace CS330_Fall2024_FinalProject.Models;
public class Athlete
{

    public int Id { get; set; }  // This is the primary key
    public string Name { get; set; }

    public int Number { get; set; }

    public string SkiLevel { get; set; }
    //Add fields for other data we want stored in each athlete model
    public SkiStats Stats { get; set; }
    public DateTime Birthday { get; set; } 

    public byte[] ProfilePicture { get; set; }

    public Athlete(int number) { } //paramaterless constructor that EF Core Expects
    public Athlete(int number, string name, SkiStats stats, string skilevel, DateTime birthday, byte[] profilePicture)
    {
        Number = number;
        Name = name;
        Stats = stats;
        SkiLevel = skilevel;
        Birthday = birthday;
        ProfilePicture = profilePicture;
    }
}




