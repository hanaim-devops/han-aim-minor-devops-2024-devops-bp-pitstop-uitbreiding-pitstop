using PitStop.WebApp.Controllers;

namespace Pitstop.WebApp.Controllers;

public class MaintenanceHistoryController : Controller
{
    private IMaintenanceHistoryAPI _maintenanceHistoryAPI;
    
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public MaintenanceHistoryController(IMaintenanceHistoryAPI maintenanceHistoryAPI,
        ILogger<MaintenanceHistoryController> logger)
    {
        _maintenanceHistoryAPI = maintenanceHistoryAPI;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index(string licenseNumber)
    {
        var maintenanceHistory = await _maintenanceHistoryAPI.GetHistoryByLicenseNumber(licenseNumber);
    
        var model = new MaintenanceHistoryViewModel
        {
            LicenseNumber = licenseNumber
        };
        return View(model);
    }
}