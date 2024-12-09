using System.ComponentModel.DataAnnotations;

namespace CS330_Fall2024_FinalProject.Models;
public class CreateAthleteViewModel
{
    public required string Name { get; set; }
    public string Id { get; set; }
    
    [Required]
    public int Number { get; set; }
    public string? SkiLevel { get; set; } = null;
    public IFormFile? ProfilePicture { get; set; } // For uploading
    public byte[]? ProfilePictureBytes { get; set; } // For displaying
    public DateTime Birthday { get; set; } 
    public double BestTime { get; set; }
    public double TopSpeed { get; set; }
    public double BestDistance { get; set; }
    public double VerticalDrop { get; set; }
    public double Ranking { get; set; }
    
}
