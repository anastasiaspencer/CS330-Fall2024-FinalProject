using CS330_Fall2024_FinalProject.Core.Repositories;
using CS330_Fall2024_FinalProject.Data;
using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CS330_Fall2024_FinalProject.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context){
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}