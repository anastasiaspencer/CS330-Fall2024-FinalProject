using CS330_Fall2024_FinalProject.Models;

public interface IAthleteService{
    Task AddAsync(Athlete athlete); //  adding a new athlete <- will need to call this on registration??
    Task<Athlete> GetByIdAsync(int number);
    Task<List<Athlete>> GetAllAsync();
}