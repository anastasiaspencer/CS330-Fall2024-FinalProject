using CS330_Fall2024_FinalProject.Models;
using Microsoft.EntityFrameworkCore;

public class AthleteService : IAthleteService
{
    private readonly DbContext _context;

    public AthleteService(DbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Athlete athlete)
    {
        await _context.Set<Athlete>().AddAsync(athlete);
        await _context.SaveChangesAsync();
    }

    public async Task<Athlete> GetByIdAsync(int number)
    {
        return await _context.Set<Athlete>().FirstOrDefaultAsync(a => a.Number == number);
    }

    public async Task<List<Athlete>> GetAllAsync()
    {
        return await _context.Set<Athlete>().ToListAsync();
    }
}


 