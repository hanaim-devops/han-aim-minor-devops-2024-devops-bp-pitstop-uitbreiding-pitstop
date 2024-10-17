namespace Pitstop.WebApp.ViewModels;

public class MaintenanceHistoryViewModel
{
    public string LicenseNumber { get; set; }
    public List<MaintenanceHistory> MaintenanceHistories { get; set; }
}