using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models.DTO
{
    public class DIYFeedbackCreateDto
    {
        public int DIYEveningId;

        public string? CustomerName { get; set; }

        public string? Feedback { get; set; }
    }
}
