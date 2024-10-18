using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models
{
    public class DIYFeedback
    {
        [Key]
        public int DIYEveningID {  get; set; } 

        public string? CustomerName { get; set; }

        public string? Feedback {  get; set; }
    }
}
