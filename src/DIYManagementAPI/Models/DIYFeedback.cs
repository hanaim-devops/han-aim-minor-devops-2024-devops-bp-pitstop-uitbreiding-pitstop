using System.ComponentModel.DataAnnotations;

namespace DIYManagementAPI.Models
{
    public class DIYFeedback
    {
        [Key]
        public int Id {  get; set; } 

        public int DIYEveningId {  get; set; } 

        public string? CustomerName { get; set; }

        public string? Feedback {  get; set; }
    }
}