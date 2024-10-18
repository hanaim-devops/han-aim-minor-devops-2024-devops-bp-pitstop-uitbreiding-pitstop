namespace Pitstop.WebApp.Models
{
    public class DIYFeedback
    {
        public int DIYEveningId { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Display(Name = "Feedback")]
        public string Feedback { get; set; }
    }
}