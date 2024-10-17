namespace Pitstop.WebApp.ViewModels;

public class ReviewManagementNewViewModel
{
    public IEnumerable<SelectListItem> Customers { get; set; }
    public string SelectedCustomerId { get; set; }
    
    public int Stars { get; set; }
    
    
}