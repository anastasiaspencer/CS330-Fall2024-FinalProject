namespace CS330_Fall2024_FinalProject.Core.Repositories
{
    public interface IUnitOfWork
    {
        IAthleteRepository Athlete { get; }

        IRoleRepository Role { get; }
    }
}


