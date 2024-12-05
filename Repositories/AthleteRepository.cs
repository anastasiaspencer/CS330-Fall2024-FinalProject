using CS330_Fall2024_FinalProject.Core.Repositories;
using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;

namespace CS330_Fall2024_FinalProject.Repositories
{
    public class AthleteRepository : IAthleteRepository
    {
        public readonly ApplicationDbContext _context;
        public AthleteRepository(ApplicationDbContext context){
            _context = context;
        }
        public ICollection<Athlete> GetUser()
        {
            return _context.Users.ToList();
        }

        public Athlete GetUser(int id){
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}