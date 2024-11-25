using CS330_Fall2024_FinalProject.Controllers;
//Interface for service to handle API calls
public interface ISnowReportService{
    Task<SnowReportModel> GetReportAsync(string LocationName);
}