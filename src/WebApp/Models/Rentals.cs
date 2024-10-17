namespace Pitstop.WebApp.Models;

public class Rentals
{
    public Guid Id { get; set; }

    [Display(Name = "Vehicle")]
    public Vehicle Vehicle { get; set; }

    [Display(Name = "Start At")]
    public DateTime? StartDate { get; set; }
    
    [Display(Name = "Ends At")]
    public DateTime? EndDate { get; set; }
    
    [Display(Name = "Renter")]
    public Customer Customer { get; set; }
}