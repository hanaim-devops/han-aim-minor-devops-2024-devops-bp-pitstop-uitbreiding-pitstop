using Polly;

namespace PitStop.WebApp.Controllers;

public class DIYManagementController : Controller
{
    private IDIYManagementAPI _DIYManagamentAPI;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public DIYManagementController(IDIYManagementAPI DIYManagamentAPI, ILogger<WorkshopManagementController> logger)
    {
        _DIYManagamentAPI = DIYManagamentAPI;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new DIYManagementViewModel
            {
                DIYEvening = await _DIYManagamentAPI.GetDIYEvening()
            };

            return View(model);
        }, View("Offline", new DIYManagementOfflineViewModel()));
    }

    [HttpGet]
    public IActionResult NewRegistration(int diyAvondId)
    {
        var model = new DIYManagementNewRegistrationViewModel
        {
            DIYRegistration = new DIYRegistration
            {
                DIYEveningId = diyAvondId
            }
        };
        return View(model);
    }

    public IActionResult New()
    {
        var model = new DIYNewViewModel
        {
            DIYEvening = new DIYEvening(),
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromForm] DIYManagementNewRegistrationViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                RegisterDIYRegistration cmd = inputModel.MapToDIYRegistration();
                await _DIYManagamentAPI.RegisterDIYAvondCustomer(cmd);
                return RedirectToAction("Index");
            }, View("Offline", new CustomerManagementOfflineViewModel()));
        }
        else
        {
            return View("NewRegistration", inputModel);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateDIYEvening([FromForm] DIYNewViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
 
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                RegisterDIYEvening cmd = inputModel.MapToRegisterEvening();

                await _DIYManagamentAPI.RegisterDIYEvening(cmd);

                return RedirectToAction("Index");
            }, View("Offline", new DIYManagementOfflineViewModel()));
        }
        else
        {
            return View("New", inputModel);
        }
    }
    public IActionResult Error()
    {
        return View();
    }
}