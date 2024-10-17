using Pitstop.MaintenanceHistoryAPI.Enums;

namespace Pitstop.WebApp.ViewModels;

public class MaintenanceHistoryDetailsViewModel
{
    public int Id { get; set; }
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public MaintenanceTypes MaintenanceType { get; set; }
    public bool IsCompleted { get; set; }
}