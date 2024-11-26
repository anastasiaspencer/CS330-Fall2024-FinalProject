using CS330_Fall2024_FinalProject.Models;
using CS330_Fall2024_FinalProject.Infrastructure;
using Newtonsoft.Json;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
//Handles API interactions and maps the responses to the domain model -> implementation of ISnowReportService
public class SnowReportService : ISnowReportService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private double _latitude {get; set;}
    private double _longitude {get; set;}

    public SnowReportService(HttpClient httpClient, IConfiguration configuration){
        _httpClient = httpClient;
        _configuration = configuration;
        //default lat and longs 
        _latitude = 44.34;
        _longitude = 10.99;
    }

    void GetLocationLatLong(string locationName){
        switch (locationName)
        {
            case "Mt.Rose Ski Tahoe":
                _latitude = 39.315;
                _longitude = 119.882;
            break;
            case "Breckenridge, Colorado":
                _latitude = 39.481;
                _longitude = 106.038;
            break;
            case "Alta Ski Area":
                _latitude = 40.577;
                _longitude = 111.624;
                break;
            default: 
            break;
        }
        return;
    }

    public async Task<SnowReportModel> GetReportAsync(string locationName)
    { 
        Console.WriteLine(locationName + "location???");
        GetLocationLatLong(locationName);
        var apiKey = _configuration["OpenWeatherMap:ApiKey"];
        var baseUrl = _configuration["OpenWeatherMap:BaseUrl"];
        var url = $"{baseUrl}forecast?lat={_latitude}&lon={_longitude}&appid={apiKey}&units=metric";
        var response = await _httpClient.GetAsync(url);

        if(response.IsSuccessStatusCode){
            var data = await response.Content.ReadFromJsonAsync<SnowReportResponseDto>();
             Console.WriteLine("Forecast Data: " + JsonConvert.SerializeObject(data.List));
        
            // check if data is returned
            if(data.List == null || !data.List.Any()){
                return new SnowReportModel
                {

                LocationName = locationName,
                Temperature = 0,
                WeatherDescription = "No forecast available",
   

                };
            }
            //firstForecast will not be null once it gets to this point
            var firstForecast = data.List.FirstOrDefault();

            return new SnowReportModel
            {
                
            
            LocationName = locationName,
            Temperature = firstForecast?.Main.Temp ?? 0, 
            FeelsLike = firstForecast?.Main?.FeelsLike ?? 0,
            WeatherDescription = firstForecast?.Weather.FirstOrDefault()?.Description ?? "No description available", 
            LowTemp = firstForecast?.Main.TempMin ?? 0,
            HighTemp = firstForecast?.Main.TempMax ?? 0,
            Pressure = firstForecast?.Main.Pressure ?? 0,
            Humidity = firstForecast?.Main.Humidity ?? 0

            };
        }

        throw new Exception("Error fetching weather data.");
    }
}