using System;

public class SnowReportModel
{

    public required string LocationName { get; set; }
    public double Temperature { get; set; }
    public required string WeatherDescription { get; set; }

    public double LowTemp { get; set; }
    public double HighTemp { get; set; }
    public double FeelsLike { get; set; }

    public double Pressure { get; set; }

    public double Humidity { get; set; }    


}




