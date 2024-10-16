namespace Pitstop.WorkshopManagementAPI.Domain.Entities;

public class MaintenanceHistory : Entity<Guid>
{
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public Guid MaintenanceJobId { get; set; }
    
    public MaintenanceHistory(Guid id) : base(id)
    {

    }
}