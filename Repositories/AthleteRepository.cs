using CS330_Fall2024_FinalProject.Core.Repositories;
using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;

namespace CS330_Fall2024_FinalProject.Repositories
{
    public class AthleteRepository : IAthleteRepository
    {
        public readonly ApplicationDbContext _context;
        public AthleteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<Athlete> GetUser()
        {
            return _context.Users.ToList();
        }

        // public Athlete GetUser(string id)
        // {
        //     return _context.Users.FirstOrDefault(u => u.Id == id);
        // }

        public Athlete GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id)
                ?? throw new InvalidOperationException($"Athlete with ID {id} not found.");
        }

        public async Task Delete(Athlete athlete)
        {
            _context.Users.Remove(athlete);
            await _context.SaveChangesAsync();
        }
        public Athlete UpdateAthlete(Athlete athlete)
        {
            _context.Update(athlete);
            _context.SaveChanges();
            return athlete;
        }
    }
}