namespace Pitstop.WebApp.Commands
{
    public class RegisterDIYRegistration : Command
    {
        public readonly string DIYAvondId;

        public readonly string CustomerName;

        public readonly string ReparationType;

        public RegisterDIYRegistration(Guid messageId, string diyAvondId, string customerName, string reperationType) : base(messageId)
        {
            DIYAvondId = diyAvondId;
            CustomerName = customerName;
            ReparationType = reperationType;
        }
    }
}
