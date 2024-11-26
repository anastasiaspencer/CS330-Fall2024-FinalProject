using CS330_Fall2024_FinalProject.Controllers;
using CS330_Fall2024_FinalProject.Models;
//Provides connection between the domain and presentation layer, coordinates flow of data
public class AthleteApplicationService
{
    private readonly IAthleteService _athleteService;

    public AthleteApplicationService(IAthleteService athleteService)
    {
        _athleteService = athleteService;
    }

   public async Task AddAthleteAsync(int number, string name, SkiStats stats)
    {
        var athlete = new Athlete(number, name, stats);
        await _athleteService.AddAsync(athlete);
    }


    public async Task<Athlete> GetAthleteByNumberAsync(int number)
    {
        return await _athleteService.GetByIdAsync(number);
    }

    public async Task<List<Athlete>> GetAllAthletesAsync() // for roster purposes
    {
        return await _athleteService.GetAllAsync();
    }
}