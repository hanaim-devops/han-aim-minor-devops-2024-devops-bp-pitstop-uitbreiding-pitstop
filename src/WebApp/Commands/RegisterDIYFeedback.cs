namespace Pitstop.WebApp.Commands
{
    public class RegisterDIYFeedback : Command
    {
        public readonly int DIYEveningId;

        public readonly string CustomerName;

        public readonly string Feedback;

        public RegisterDIYFeedback(Guid messageId, int diyEveningId, string customerName, string feedback) : base(messageId)
        {
            DIYEveningId = diyEveningId;
            CustomerName = customerName;
            Feedback = feedback;
        }
    }
}
