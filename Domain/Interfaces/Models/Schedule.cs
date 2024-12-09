using System;

namespace CS330_Fall2024_FinalProject.Models;
public class Schedule
{
    public int Id { get; set; }
    public string Type {  get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }

    public Schedule() { }

    public Schedule(int id, string type, DateTime date, string description, string location)
    {
        Id = id;
        Type = type;
        Date = date;
        Description = description;
        Location = location;
    }

}

