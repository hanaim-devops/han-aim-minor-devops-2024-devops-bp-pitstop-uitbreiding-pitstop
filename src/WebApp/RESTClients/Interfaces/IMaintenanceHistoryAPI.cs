namespace WebApp.RESTClients;

public interface IMaintenanceHistoryAPI
{
    [Get("/maintenancehistory/{licenseNumber}")]
    Task<List<MaintenanceHistory>> GetHistoryByLicenseNumber(string licenseNumber);
}