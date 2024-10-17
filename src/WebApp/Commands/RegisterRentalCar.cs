namespace Pitstop.WebApp.Commands;

public class RegisterRentalCar : Command
{
    public readonly string LicenseNumber;
    public readonly string Brand;
    public readonly string Type;

    public RegisterRentalCar(Guid messageId, string licenseNumber, string brand, string type) :
        base(messageId)
    {
        LicenseNumber = licenseNumber;
        Brand = brand;
        Type = type;
    }
}