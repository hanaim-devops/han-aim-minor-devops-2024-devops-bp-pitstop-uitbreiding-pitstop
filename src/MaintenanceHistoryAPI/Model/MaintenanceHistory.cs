namespace MaintenanceHistoryAPI.Model;

public class MaintenanceHistory
{
    public string LicenseNumber { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; }
    public MaintenanceTypes MaintenanceType { get; set; }
    public Guid MaintenanceJobId { get; set; }

    public enum MaintenanceTypes
    {
        Onderhoud,
        APK,
        Schadeherstel,
        Bandenwissel,
        Overig
    }
}

