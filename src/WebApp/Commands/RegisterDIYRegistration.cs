namespace Pitstop.WebApp.Commands
{
    public class RegisterDIYRegistration : Command
    {
        public readonly int DIYEveningId;

        public readonly string CustomerName;

        public readonly string Reparations;

        public RegisterDIYRegistration(Guid messageId, int diyEveningId, string customerName, string reparations) : base(messageId)
        {
            DIYEveningId = diyEveningId;
            CustomerName = customerName;
            Reparations = reparations;
        }
    }
}
