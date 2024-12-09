using CS330_Fall2024_FinalProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CS330_Fall2024_FinalProject.Repositories
{
    public class SkiStatsRepository
    {
        private readonly ApplicationDbContext _context;

        public SkiStatsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public (double Value, string? AthleteName) GetBestTime()
        {
            var bestTime = _context.SkiStats
                .OrderBy(s => s.BestTime)
                .Select(s => new
                {
                    Value = s.BestTime,
                    statId = s.Id
                }).FirstOrDefault();

            if (bestTime == null)
            {
                return (0, "Unknown");
            }

            var athleteName = _context.Users
                .Where(u => u.StatsId == bestTime.statId)
                .Select(u => u.Name)
                .FirstOrDefault();

            return (bestTime.Value, athleteName);
        }

        public (double Value, string? AthleteName) GetTopSpeed()
        {
            var topSpeed = _context.SkiStats
                .OrderByDescending(s => s.TopSpeed)
                .Select(s => new
                {
                    Value = s.TopSpeed,
                    statId = s.Id
                }).FirstOrDefault();

            if (topSpeed == null)
            {
                return (0, "Unknown");
            }

            var athleteName = _context.Users
                .Where(u => u.StatsId == topSpeed.statId)
                .Select(u => u.Name)
                .FirstOrDefault();

            return (topSpeed.Value, athleteName);
        }

        public (double Value, string? AthleteName) GetBestDistance()
        {
            var bestDistance = _context.SkiStats
                .OrderByDescending(s => s.BestDistance)
                .Select(s => new
                {
                    Value = s.BestDistance,
                    statId = s.Id
                }).FirstOrDefault();

            if (bestDistance == null)
            {
                return (0, "Unknown");
            }

            var athleteName = _context.Users
                .Where(u => u.StatsId == bestDistance.statId)
                .Select(u => u.Name)
                .FirstOrDefault();

            return (bestDistance.Value, athleteName);
        }

        public (double Value, string? AthleteName) GetVerticalDrop()
        {
            var verticalDrop = _context.SkiStats
                .OrderByDescending(s => s.VerticalDrop)
                .Select(s => new
                {
                    Value = s.VerticalDrop,
                    statId = s.Id
                }).FirstOrDefault();

            if (verticalDrop == null)
            {
                return (0, "Unknown");
            }

            var athleteName = _context.Users
                .Where(u => u.StatsId == verticalDrop.statId)
                .Select(u => u.Name)
                .FirstOrDefault();

            return (verticalDrop.Value, athleteName);
        }

        public List<(string StatName, double Average)> GetTeamAverages()
        {
            return new List<(string StatName, double Average)>
    {
        ("Best Time", _context.SkiStats.Average(s => s.BestTime)),
        ("Top Speed", _context.SkiStats.Average(s => s.TopSpeed)),
        ("Best Distance", _context.SkiStats.Average(s => s.BestDistance)),
        ("Vertical Drop", _context.SkiStats.Average(s => s.VerticalDrop))
    };
        }

        public (double Value, string AthleteName) GetAthleteBestTime(string athleteId)
        {
            var athlete = _context.Users
                .Where(u => u.Id == athleteId)
                .Select(u => new
                {
                    Name = u.Name,
                    StatsId = u.StatsId
                })
                .FirstOrDefault();

            if (athlete == null || athlete.StatsId == null)
            {
                return (0, "Unknown");
            }

            var bestTime = _context.SkiStats
                .Where(s => s.Id == athlete.StatsId)
                .Select(s => s.BestTime)
                .FirstOrDefault();

            return (bestTime, athlete.Name);
        }

        public (double Value, string AthleteName) GetAthleteTopSpeed(string athleteId)
        {
            var athlete = _context.Users
                .Where(u => u.Id == athleteId)
                .Select(u => new
                {
                    Name = u.Name,
                    StatsId = u.StatsId
                })
                .FirstOrDefault();

            if (athlete == null || athlete.StatsId == null)
            {
                return (0, "Unknown");
            }

            var topSpeed = _context.SkiStats
                .Where(s => s.Id == athlete.StatsId)
                .Select(s => s.TopSpeed)
                .FirstOrDefault();

            return (topSpeed, athlete.Name);
        }

        public (double Value, string AthleteName) GetAthleteBestDistance(string athleteId)
        {
            var athlete = _context.Users
                .Where(u => u.Id == athleteId)
                .Select(u => new
                {
                    Name = u.Name,
                    StatsId = u.StatsId
                })
                .FirstOrDefault();

            if (athlete == null || athlete.StatsId == null)
            {
                return (0, "Unknown");
            }

            var bestDistance = _context.SkiStats
                .Where(s => s.Id == athlete.StatsId)
                .Select(s => s.BestDistance)
                .FirstOrDefault();

            return (bestDistance, athlete.Name);
        }

        public (double Value, string AthleteName) GetAthleteVerticalDrop(string athleteId)
        {
            var athlete = _context.Users
                .Where(u => u.Id == athleteId)
                .Select(u => new
                {
                    Name = u.Name,
                    StatsId = u.StatsId
                })
                .FirstOrDefault();

            if (athlete == null || athlete.StatsId == null)
            {
                return (0, "Unknown");
            }

            var verticalDrop = _context.SkiStats
                .Where(s => s.Id == athlete.StatsId)
                .Select(s => s.VerticalDrop)
                .FirstOrDefault();

            return (verticalDrop, athlete.Name);
        }


        public (string AthleteName, byte[]? ProfilePictureBytes) GetAthleteProfile(string athleteId)
        {
            var athlete = _context.Users
                .Where(u => u.Id == athleteId)
                .Select(u => new
                {
                    AthleteName = u.Name,
                    ProfilePictureBytes = u.ProfilePicture
                })
                .FirstOrDefault();

            if (athlete == null)
            {
                return ("Unknown", null);
            }

            return (athlete.AthleteName, athlete.ProfilePictureBytes);
        }


    }
}
