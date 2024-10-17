namespace Pitstop.WebApp.Commands
{
    public class RegisterDIYRegistration : Command
    {
        public readonly int DIYAvondId;

        public readonly string CustomerName;

        public readonly string ReparationType;

        public RegisterDIYRegistration(Guid messageId, int diyAvondId, string customerName, string reperationType) : base(messageId)
        {
            DIYAvondId = diyAvondId;
            CustomerName = customerName;
            ReparationType = reperationType;
        }
    }
}
