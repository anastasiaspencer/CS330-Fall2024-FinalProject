using CS330_Fall2024_FinalProject.Core.Repositories;
using CS330_Fall2024_FinalProject.Data;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;

namespace CS330_Fall2024_FinalProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAthleteRepository Athlete {get;}
        public IRoleRepository Role {get;}
        public UnitOfWork(IAthleteRepository athlete, IRoleRepository role)
        {
            Athlete = athlete;
            Role = role;
        }
    }
}