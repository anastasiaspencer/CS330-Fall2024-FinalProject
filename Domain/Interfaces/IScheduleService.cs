using CS330_Fall2024_FinalProject.Models;

public interface IScheduleService
{
    Task AddAsync(Schedule schedule); //  adding a new athlete <- will need to call this on registration??
    // Task<Athlete> GetByIdAsync(int number); //TODO Implement this
    Task<List<Schedule>> GetAllAsync();

    Task DeleteAsync(int id);
}