namespace RentalCarManagementAPI.Models;

public class RentalCar
{
    public string Id { get; set; }
    public string LicenseNumber { get; set; }
    public Model Model { get; set; }
    public string ModelId { get; set; }
}