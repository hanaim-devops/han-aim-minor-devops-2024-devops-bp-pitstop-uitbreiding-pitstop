namespace Pitstop.RentalManagementAPI.Responses;

public class RentalCarResponse
{
    public string Id { get; set; }
    public string LicenseNumber { get; set; }
    public ModelResponse Model { get; set; }
}