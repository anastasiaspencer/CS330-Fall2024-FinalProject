using System;
namespace CS330_Fall2024_FinalProject.Models;

public class Athlete
{

    public int Id { get; set; }  // This is the primary key
    public string Name  { get; set; } = "Unknown";
    public int Number { get; set; }
    //Add fields for other data we want stored in each athlete model
    public SkiStats Stats { get; set; } = new SkiStats();

    public Athlete(){} //paramaterless constructor that EF Core Expects
    public Athlete(int number, string name, SkiStats stats)
    {
        Number = number;
        Name = name;
        Stats = stats;
    }
}




