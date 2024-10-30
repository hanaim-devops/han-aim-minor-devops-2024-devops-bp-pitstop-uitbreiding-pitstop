namespace Pitstop.WebApp.Commands;

public class ExtendRental : Command
{
    public readonly DateTime EndDate;
    
    public ExtendRental(Guid messageId, DateTime endDate) : base(messageId)
    {
        EndDate = endDate;
    }
}