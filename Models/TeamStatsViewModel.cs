namespace CS330_Fall2024_FinalProject.Models
{
    public class TeamStatsViewModel
    {
        public List<SkiStatsViewModel> TeamRecords { get; set; }
        public List<(string StatName, double Average)> TeamAverages { get; set; }
    }

}
