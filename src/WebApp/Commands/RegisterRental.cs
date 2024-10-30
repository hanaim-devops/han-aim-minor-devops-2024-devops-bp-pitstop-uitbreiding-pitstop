namespace Pitstop.WebApp.Commands;

public class RegisterRental : Command
{
    public readonly string CarId;
    public readonly string CustomerId;
    public readonly DateTime StartDate;
    public readonly DateTime EndDate;

    public RegisterRental(Guid messageId, string carId, string customerId, DateTime startDate, DateTime endDate) :
        base(messageId)
    {
        CarId = carId;
        CustomerId = customerId;
        StartDate = startDate;
        EndDate = endDate;
    }
}