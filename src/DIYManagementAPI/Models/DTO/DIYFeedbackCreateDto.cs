namespace DIYManagementAPI.Models.DTO
{
    public class DIYFeedbackCreateDto
    {
        public int DIYEveningId { get; set; }

        public required string CustomerName { get; set; }

        public required string Feedback { get; set; }
    }
}
