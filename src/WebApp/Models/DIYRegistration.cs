namespace Pitstop.WebApp.Models
{
    public class DIYRegistration
    {
        public int DIYEveningId { get; set; }

        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Display(Name = "Reparations")]
        public string Reparations { get; set; }
    }
}
