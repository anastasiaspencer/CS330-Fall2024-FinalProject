using CS330_Fall2024_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CS330_Fall2024_FinalProject.Core.Repositories
{
    public interface IAthleteRepository
    {
        ICollection<Athlete> GetUser();

        Athlete GetUser(string id);

        Athlete UpdateAthlete(Athlete athlete);

        Task Delete(Athlete athlete);
    }
}


