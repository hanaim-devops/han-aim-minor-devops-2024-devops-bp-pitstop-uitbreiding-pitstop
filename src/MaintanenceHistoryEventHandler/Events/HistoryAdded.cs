namespace Pitstop.MaintanenceHistoryEventHandler.Events;

public class HistoryAdded
{
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public Guid MaintenanceJobId { get; set; }
}