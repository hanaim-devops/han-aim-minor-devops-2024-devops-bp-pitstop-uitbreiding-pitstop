namespace WebApp.RESTClients;

public interface IMaintenanceHistoryAPI
{
    [Get("/maintenancehistory/{id}")]
    Task<MaintenanceHistory> GetHistoryById(int id);
    
    [Get("/maintenancehistory/{licenseNumber}")]
    Task<List<MaintenanceHistory>> GetHistoryByLicenseNumber(string licenseNumber);
}