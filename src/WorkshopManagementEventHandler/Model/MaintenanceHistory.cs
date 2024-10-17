using System.ComponentModel;
using MaintenanceHistoryAPI.Enums;

namespace Pitstop.WorkshopManagementEventHandler.Model;

public class MaintenanceHistory
{
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string? Description { get; set; }
    
    public MaintenanceTypes MaintenanceType { get; set; }
    
    public Guid MaintenanceJobId { get; set; }
    
    [DefaultValue("false")]
    public bool IsCompleted { get; set; }
}

