namespace Pitstop.WebApp.ViewModels;

public class ReviewManagementNewViewModel
{
    public IEnumerable<SelectListItem> Customers { get; set; }
    public string SelectedCustomer { get; set; }
    
    public string ReviewComment { get; set; }
    public int Stars { get; set; }
    
    
}