namespace CS330_Fall2024_FinalProject.Infrastructure
{
    //Represents the structure of the response recieved from the API 
    public class WeatherInfo
    {
        public string Description { get; set; }
    }
    // For whatever reason, FeelsLike, TempMin and TempMax all show as 0 
    public class MainWeather
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }

         public int Pressure { get; set; }
        public int Humidity { get; set; }
       
    }

    public class Forecast
    {
        public int Dt { get; set; }
        public MainWeather Main { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public string DtTxt { get; set; }
    }

    public class SnowReportResponseDto
    {
        public List<Forecast> List { get; set; }
    }

}