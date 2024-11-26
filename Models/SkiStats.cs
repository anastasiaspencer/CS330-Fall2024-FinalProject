public class SkiStats{
    public double BestTime { get; set; } 
    public double TopSpeed { get; set; } 
    public double BestDistance { get; set; }
    public double VerticalDrop { get; set; } 
    public double Ranking { get; set; } //

    public SkiStats(double time, double speed, double distance, double verticalDrop, double ranking)
    {
        BestTime = time;
        TopSpeed = speed;
        BestDistance = distance;
        VerticalDrop = verticalDrop;
        Ranking = ranking;
    }
}