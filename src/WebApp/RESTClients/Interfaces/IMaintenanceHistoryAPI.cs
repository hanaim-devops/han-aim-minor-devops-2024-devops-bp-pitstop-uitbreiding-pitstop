namespace WebApp.RESTClients;

public interface IMaintenanceHistoryAPI
{
    [Get("/maintenancehistory/{licenseNumber}")]
    Task<MaintenanceHistory> GetHistoryById(string licenseNumber);
}