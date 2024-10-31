namespace Pitstop.WebApp.Models;

public class Rental
{
    public string Id { get; set; }

    public string CarId { get; set; }
    
    [Display(Name = "Car")]
    public RentalCar Car { get; set; }

    [Display(Name = "Start At")]
    public DateTime StartDate { get; set; }

    [Display(Name = "Ends At")]
    public DateTime EndDate { get; set; }

    public string CustomerId { get; set; }

    [Display(Name = "Renter")]
    public Customer Customer { get; set; }
}