using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CS330_Fall2024_FinalProject.Core.Repositories
{
    public interface IRoleRepository{
        ICollection<IdentityRole> GetRoles();
    }
}