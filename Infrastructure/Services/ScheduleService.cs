using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SQLitePCL;

public class ScheduleService : IScheduleService
{
    private readonly ApplicationDbContext _context;

    public ScheduleService (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Schedule schedule)
    {
        await _context.Set<Schedule>().AddAsync(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Schedule>> GetAllAsync()
    {
        return await _context.Set<Schedule>().ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var schedule = await _context.Schedules.FindAsync(id);
        if(schedule != null)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"Schedule with ID {id} not found.");
        }
    }
}