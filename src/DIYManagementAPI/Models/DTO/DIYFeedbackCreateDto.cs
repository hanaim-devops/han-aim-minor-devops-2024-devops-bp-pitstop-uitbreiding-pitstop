using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models.DTO
{
    public class DIYFeedbackCreateDto
    {
        [Required]
        public required string CustomerName { get; set; }

        [Required]
        public required string Feedback { get; set; }
    }
}
