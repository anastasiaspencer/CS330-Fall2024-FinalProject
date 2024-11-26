using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.EntityFrameworkCore;
//Part that actually talks to the database -> implementation of IAthleteService interface
public class AthleteService : IAthleteService
{
    private readonly ApplicationDbContext _context;

    public AthleteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Athlete athlete)
    {
        await _context.Set<Athlete>().AddAsync(athlete);
        await _context.SaveChangesAsync();
    }

    // public async Task<Athlete> GetByIdAsync(int number) //IMPLEMENT THIS
    // {
    //     return await _context.Set<Athlete>().FirstOrDefaultAsync(a => a.Number == number);
    // }

    // public async Task<List<Athlete>> GetAllAsync() //IMPLEMENT THIS
    // {
    //     return await _context.Set<Athlete>().ToListAsync();
    // }
}


 