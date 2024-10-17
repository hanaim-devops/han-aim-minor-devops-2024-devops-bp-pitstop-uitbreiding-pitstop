using Pitstop.MaintenanceHistoryAPI.Enums;

namespace Pitstop.WebApp.Models;

public class MaintenanceHistory
{
    public int Id { get; set; }
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public MaintenanceTypes MaintenanceType { get; set; }
    public bool IsCompleted { get; set; }
    public Guid MaintenanceJobId { get; set; }
}