using CS330_Fall2024_FinalProject.Controllers;
using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
//Provides connection between the domain and presentation layer, coordinates flow of data
public class AthleteApplicationService
{
    private readonly IAthleteService _athleteService;
    public AthleteApplicationService(IAthleteService athleteService )
    {
        _athleteService = athleteService;
      
    }

//   public async Task AddAthleteAsync(CreateAthleteViewModel model)
// {
   
//     // Map the ViewModel to domain models
//     var skiStats = new SkiStats(
//         model.BestTime,
//         model.TopSpeed,
//         model.BestDistance,
//         model.VerticalDrop,
//         model.Ranking
//     );
//     var athlete = new Athlete(model.Number, model.Name, skiStats, model.SkiLevel, model.Birthday, model.ProfilePicture); //model.ProfilePicture

    
//     await _athleteService.AddAsync(athlete);
// }

public async Task AddAthleteAsync(CreateAthleteViewModel model)
{
    // Convert IFormFile to byte[] (if not already done in the controller)
    byte[]? profilePictureBytes = null;
    if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            await model.ProfilePicture.CopyToAsync(memoryStream);
            profilePictureBytes = memoryStream.ToArray();
        }
    }

    // Map the ViewModel to domain models
    var skiStats = new SkiStats(
        model.BestTime,
        model.TopSpeed,
        model.BestDistance,
        model.VerticalDrop,
        model.Ranking,
        profilePictureBytes
    );

    var athlete = new Athlete(
        model.Number,
        model.Name,
        skiStats,
        model.SkiLevel,
        model.Birthday, 
        profilePictureBytes
    );

    await _athleteService.AddAsync(athlete);
}

    public async Task DeleteAthleteAsync(int id){
        await _athleteService.DeleteAsync(id);
    }

    // public async Task<Athlete> GetAthleteByNumberAsync(int number) //just an idea of something we migt want to implement
    // {
    //     return await _athleteService.GetByIdAsync(number);
    // }

    public async Task<List<Athlete>> GetAllAthletesAsync() // for roster purposes
    {
        return await _athleteService.GetAllAsync();
    }
    
}