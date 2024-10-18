namespace Pitstop.WebApp.Commands
{
    public class RegisterDIYEvening : Command
    {
        public readonly string Title;
        public readonly string ExtraInfo;
        public readonly DateTime StartDate;
        public readonly DateTime EndDate;
        public readonly string Mechanic;


        public RegisterDIYEvening(Guid messageId, string title, string extraInfo, 
            DateTime startDate, DateTime endDate, string mechanic) :
             base(messageId)
        {
            Title = title;
            ExtraInfo = extraInfo;
            StartDate = startDate;
            EndDate = endDate;
            Mechanic = mechanic;
        }
    }
}
