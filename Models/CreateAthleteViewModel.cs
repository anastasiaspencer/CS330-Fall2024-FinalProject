using System.ComponentModel.DataAnnotations;

namespace CS330_Fall2024_FinalProject.Models;
public class CreateAthleteViewModel
{
    public required string Name { get; set; }
    public int Id { get; set; }
    
     [Required]
     public int Number { get; set; }
    public string? SkiLevel { get; set; } = null;
    
    // public byte[]? ProfilePicture { get; set; } = null;
    public DateTime Birthday { get; set; } 
    public double BestTime { get; set; }
    public double TopSpeed { get; set; }
    public double BestDistance { get; set; }
    public double VerticalDrop { get; set; }
    public double Ranking { get; set; }
}
