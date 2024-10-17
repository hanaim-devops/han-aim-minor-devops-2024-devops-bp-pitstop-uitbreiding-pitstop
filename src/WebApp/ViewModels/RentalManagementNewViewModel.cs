namespace Pitstop.WebApp.ViewModels;

public class RentalManagementNewViewModel
{
    public Guid Id { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [Required]
    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public IEnumerable<SelectListItem> Vehicles { get; set; }

    [Required(ErrorMessage = "Vehicle is required")]
    [Display(Name = "Vehicle")]
    public string SelectedVehicleLicenseNumber { get; set; }

    public IEnumerable<SelectListItem> Customers { get; set; }
    
    [Required(ErrorMessage = "Owner is required")]
    public string SelectedCustomerId { get; set; }
    
    public string Error { get; set; }
}