using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLitePCL;
//Part that actually talks to the database -> implementation of IAthleteService interface
public class AthleteService : IAthleteService
{
    private readonly ApplicationDbContext _context;

    public AthleteService(ApplicationDbContext context)
    {
        _context = context;
    }

    // public async Task AddAsync(Athlete athlete)
    // {
    //     Console.WriteLine($"Athlete: {JsonConvert.SerializeObject(athlete)}");

    // if (athlete.ProfilePicture != null && athlete.ProfilePicture.Length > 0)
    //     {
    //         if (athlete.ProfilePicture.Length > 2 * 1024 * 1024) // 2 MB limit
    //         {
    //             throw new InvalidOperationException("Profile picture exceeds the size limit of 2 MB.");
    //         }
    //         Console.WriteLine("Profile picture detected and will be saved.");
    //     }
    //     else
    //     {
    //         Console.WriteLine("No profile picture provided.");
    //     }

    //    // Console.WriteLine("-------********------------------------------", athlete.SkiLevel);
    //    Console.WriteLine($"Ski Level: {athlete.SkiLevel}");

    //     await _context.Set<Athlete>().AddAsync(athlete);
    //     await _context.SaveChangesAsync();
    // }

    public async Task AddAsync(Athlete athlete)
{
    Console.WriteLine($"Athlete: {JsonConvert.SerializeObject(athlete)}");

    if (athlete.ProfilePicture == null || athlete.ProfilePicture.Length == 0)
    {
        Console.WriteLine("No profile picture provided.");
    }
    else if (athlete.ProfilePicture.Length > 2 * 1024 * 1024) // 2 MB limit
    {
        throw new InvalidOperationException("Profile picture exceeds the size limit of 2 MB.");
    }
    else
    {
        Console.WriteLine("Profile picture detected and will be saved.");
    }

    Console.WriteLine($"Ski Level: {athlete.SkiLevel}");

    try
    {
        await _context.Set<Athlete>().AddAsync(athlete);
        await _context.SaveChangesAsync();
        Console.WriteLine("Athlete successfully added to the database.");
    }
    catch (DbUpdateException ex)
    {
        Console.WriteLine($"Error adding athlete to the database: {ex.Message}");
        throw;
    }
}

    // public async Task<Athlete> GetByIdAsync(string name) //IMPLEMENT THIS
    // {
    //     return await _context.Set<Athlete>().FirstOrDefaultAsync(a => a.Name == name);
    // }

    public async Task<List<Athlete>> GetAllAsync() //gets all athletes in teh database
    {
        return await _context.Users.ToListAsync();
    }

    public async Task DeleteAsync(int id){
        // var athlete = _context.Athletes.Find(id);
        // _context.Athletes.Remove(athlete);
    
      
        // await _context.SaveChangesAsync();

        var athlete = await _context.Users.FindAsync(id); 
        if (athlete != null) 
        {
            _context.Users.Remove(athlete);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"Athlete with ID {id} not found.");
        }
    }
}


 