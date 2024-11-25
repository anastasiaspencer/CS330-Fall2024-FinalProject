using CS330_Fall2024_FinalProject.Controllers;
//Provides connection between the domain and presentation layer, coordinates flow of data
public class SnowReportApplicationService
{
    private readonly ISnowReportService _snowReportService;

    public SnowReportApplicationService(ISnowReportService snowReportService)
    {
        _snowReportService = snowReportService;
    }

    public async Task<SnowReportModel> GetReportAsync(string locationName){
        return await _snowReportService.GetReportAsync(locationName);
    }
}