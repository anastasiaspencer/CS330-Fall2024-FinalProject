using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
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

    // public async Task<Athlete> GetByIdAsync(string name) //IMPLEMENT THIS
    // {
    //     return await _context.Set<Athlete>().FirstOrDefaultAsync(a => a.Name == name);
    // }

    public async Task<List<Athlete>> GetAllAsync() //gets all athletes in teh database
    {
        return await _context.Set<Athlete>().ToListAsync();
    }

    public async Task DeleteAsync(string id){
        var athlete = _context.Set<Athlete>().FirstOrDefaultAsync(a => a.Name == id);
        _context.Remove(athlete);
    
      
        await _context.SaveChangesAsync();
    }
}


 