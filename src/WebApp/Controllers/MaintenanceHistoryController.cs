namespace Pitstop.WebApp.Controllers;

public class MaintenanceHistoryController : Controller
{
    private IMaintenanceHistoryAPI _maintenanceHistoryAPI;
    
    [HttpGet]
    public IActionResult Index(string licenseNumber)
    {
        var model = new MaintenanceHistoryViewModel
        {
            LicenseNumber = licenseNumber
        };
        return View(model);
    }
}