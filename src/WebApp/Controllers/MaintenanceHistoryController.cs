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
            LicenseNumber = licenseNumber,
            MaintenanceHistories = maintenanceHistory
        };
        return View(model);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var history = await _maintenanceHistoryAPI.GetHistoryById(id);

        var model = new MaintenanceHistoryDetailsViewModel
        {
            Id = history.Id,
            LicenseNumber = history.LicenseNumber,
            MaintenanceDate = history.MaintenanceDate,
            Description = history.Description,
            MaintenanceType = history.MaintenanceType,
            IsCompleted = history.IsCompleted
        };
        return View(model);
    }
}