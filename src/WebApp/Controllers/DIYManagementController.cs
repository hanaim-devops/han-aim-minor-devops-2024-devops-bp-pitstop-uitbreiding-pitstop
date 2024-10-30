using System.Diagnostics;
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
    public async Task<IActionResult> Details(int id)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new DIYManagementDetailsViewModel
            {
                DIYEvening = await _DIYManagamentAPI.GetDIYEveningById(id.ToString()),
                DIYRegistrations = await _DIYManagamentAPI.GetRegistrationsForDIYEvening(id.ToString())
            };

            return View(model);
        }, View("Offline", new DIYManagementOfflineViewModel()));
    }

    [HttpGet]
    public IActionResult NewRegistration(int diyEveningId)
    {
        var model = new DIYManagementNewRegistrationViewModel
        {
            DIYRegistration = new DIYRegistration
            {
                DIYEveningId = diyEveningId
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
                await _DIYManagamentAPI.RegisterDIYEveningCustomer(cmd);
                return RedirectToAction("Index");
            }, View("Offline", new DIYManagementOfflineViewModel()));
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

    [HttpPost]
    public async Task<IActionResult> CancelRegistration(int diyRegistrationId)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            await _DIYManagamentAPI.CancelRegistration(diyRegistrationId.ToString());
            return RedirectToAction("Index");
        }, View("Offline", new DIYManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public IActionResult NewFeedback(int diyEveningId)
    {
        var model = new DIYManagementNewFeedbackViewModel
        {
            DIYFeedback = new DIYFeedback
            {
                DIYEveningId = diyEveningId
            }
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterFeedback([FromForm] DIYManagementNewFeedbackViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                RegisterDIYFeedback cmd = inputModel.MapToDIYFeedback();
                await _DIYManagamentAPI.RegisterDIYFeedback(cmd);
                return RedirectToAction("Index");
            }, View("Offline", new CustomerManagementOfflineViewModel()));
        }
        else
        {
            return View("NewFeedback", inputModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> CustomerOverview()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var DIYEvening = await _DIYManagamentAPI.GetFutureDIYEvenings();
            var model = new DIYManagementCustomerOverviewViewModel
            {
                DIYEvening = DIYEvening,
                DIYRegistrations = await _DIYManagamentAPI.GetRegistrationsForDIYEvening(DIYEvening[0].Id.ToString())
            };

            return View(model);
        }, View("Offline", new DIYManagementOfflineViewModel()));
    }
}