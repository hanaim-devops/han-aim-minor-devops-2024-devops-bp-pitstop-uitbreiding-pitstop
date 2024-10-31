namespace Pitstop.WebApp.Commands;

public class ChangeCarRental : Command
{
    public readonly string CarId;
    
    public ChangeCarRental(Guid messageId, string carId) : base(messageId)
    {
        CarId = carId;
    }
}