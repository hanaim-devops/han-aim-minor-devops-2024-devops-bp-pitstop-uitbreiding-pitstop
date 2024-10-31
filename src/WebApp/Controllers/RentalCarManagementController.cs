namespace PitStop.WebApp.Controllers;

public class RentalCarManagementController : Controller
{
    private IRentalCarManagementAPI _rentalCarManagementApi;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public RentalCarManagementController(IRentalCarManagementAPI rentalCarManagementApi, ILogger<VehicleManagementController> logger)
    {
        _rentalCarManagementApi = rentalCarManagementApi;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new RentalCarManagementViewModel
            {
                RentalCars = await _rentalCarManagementApi.GetRentalCars()
            };
            return View(model);
        }, View("Offline", new RentalCarManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(string licenseNumber)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            RentalCar rentalCar = await _rentalCarManagementApi.GetRentalCarById(licenseNumber);

            var model = new RentalCarManagementDetailsViewModel
            {
                RentalCar = rentalCar
            };
            return View(model);
        }, View("Offline", new RentalCarManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> New()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new RentalCarManagementNewViewModel
            {
                RentalCar = new RentalCar()
            };
            return View(model);
        }, View("Offline", new RentalCarManagementOfflineViewModel()));
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RentalCarManagementNewViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                try
                {
                    RegisterRentalCar cmd = inputModel.MapToRegisterRentalCar();
                    await _rentalCarManagementApi.RegisterRentalCar(cmd);
                    return RedirectToAction("Index");
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.Conflict)
                    {
                        // add errormessage from API exception to model
                        var content = await ex.GetContentAsAsync<BusinessRuleViolation>();
                        inputModel.Error = content.ErrorMessage;
                        
                        // back to New view
                        return View("New", inputModel);
                    }
                }
                
                return RedirectToAction("Index");
            }, View("Offline", new RentalCarManagementOfflineViewModel()));
        }
        else
        {
            return View("New", inputModel);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(string rentalCarId)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            try
            {
                await _rentalCarManagementApi.DeleteReview(rentalCarId);
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return NotFound("Rental car not found.");
                else if (ex.StatusCode == HttpStatusCode.Conflict)
                    return Conflict("Unable to delete rental car due to a conflict.");
            }

            return RedirectToAction("Index");
        }, View("Offline", new RentalCarManagementOfflineViewModel()));
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }
}