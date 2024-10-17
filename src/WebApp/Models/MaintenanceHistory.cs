namespace Pitstop.WebApp.Models;

public class MaintenanceHistory
{
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public Guid MaintenanceJobId { get; set; }
}