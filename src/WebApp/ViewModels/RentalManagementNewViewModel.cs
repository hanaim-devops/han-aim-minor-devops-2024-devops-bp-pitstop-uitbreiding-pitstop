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

    public IEnumerable<SelectListItem> RentalCars { get; set; }

    [Required(ErrorMessage = "Car is required")]
    [Display(Name = "Car")]
    public string SelectedRentalCarLicenseNumber { get; set; }

    public IEnumerable<SelectListItem> Customers { get; set; }
    
    [Required(ErrorMessage = "Owner is required")]
    public string SelectedCustomerId { get; set; }
    
    public string Error { get; set; }
}