namespace PitStop.WebApp.Controllers;

public class RentalManagementController : Controller
{
    // private IRentalManagementAPI _rentalManagementAPI;
    private ICustomerManagementAPI _customerManagementAPI;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public RentalManagementController(ICustomerManagementAPI customerManagementAPI, ILogger<RentalManagementController> logger)
    // public RentalManagementController(IRentalManagementAPI rentalManagementAPI, ICustomerManagementAPI customerManagementAPI, ILogger<RentalManagementController> logger)
    {
        // _rentalManagementAPI = rentalManagementAPI;
        _customerManagementAPI = customerManagementAPI;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new RentalManagementViewModel
            {
                Rentals = new List<Rentals>()
            };

            // Rentals rentals = await _rentalManagementAPI.GetRentals();
            // if (rentals?.Count > 0)
            // {
            //     model.Rentals.AddRange(rentals.OrderBy(r => r.StartDate));
            // }

            return View(model);
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }
    
    
    [HttpGet]
    public async Task<IActionResult> New()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            DateTime currentDate = DateTime.Today;
            var customers = await _customerManagementAPI.GetCustomers();

            var model = new RentalManagementNewViewModel
            {
                Id = Guid.NewGuid(),
                StartDate = currentDate,
                EndDate = currentDate.AddDays(1),
                // Vehicles = await GetAvailableVehiclesList()
                Customers = customers.Select(c => new SelectListItem { Value = c.CustomerId, Text = c.Name })
            };
            return View(model);
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMaintenanceJob([FromForm] RentalManagementNewViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                try
                {
                    //     // register maintenance job
                    //     DateTime startTime = inputModel.Date.Add(inputModel.StartTime.TimeOfDay);
                    // DateTime endTime = inputModel.Date.Add(inputModel.EndTime.TimeOfDay);
                    // Vehicle vehicle = await _workshopManagementAPI.GetVehicleByLicenseNumber(inputModel.SelectedVehicleLicenseNumber);
                    // Customer customer = await _workshopManagementAPI.GetCustomerById(vehicle.OwnerId);
                    //
                    // PlanMaintenanceJob planMaintenanceJobCommand = new PlanMaintenanceJob(Guid.NewGuid(), Guid.NewGuid(), startTime, endTime,
                    //     (customer.CustomerId, customer.Name, customer.TelephoneNumber),
                    //     (vehicle.LicenseNumber, vehicle.Brand, vehicle.Type), inputModel.Description);
                    // await _workshopManagementAPI.PlanMaintenanceJob(dateStr, planMaintenanceJobCommand);
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.Conflict)
                    {
                            // add errormessage from API exception to model
                            var content = await ex.GetContentAsAsync<BusinessRuleViolation>();
                        inputModel.Error = content.ErrorMessage;

                            // repopulate list of available vehicles in the model
                            inputModel.Vehicles = await GetAvailableVehiclesList();

                            // back to New view
                            return View("New", inputModel);
                    }
                }

                return RedirectToAction("Index");
            }, View("Offline", new RentalManagementOfflineViewModel()));
        }
        else
        {
            // inputModel.Vehicles = await GetAvailableVehiclesList();
            return View("New", inputModel);
        }
    }
    
    private async Task<IEnumerable<SelectListItem>> GetAvailableVehiclesList()
    {
        return null;
        // var vehicles = await _rentalManagementAPI.GetVehicles();
        // return vehicles.Select(v =>
        //     new SelectListItem
        //     {
        //         Value = v.LicenseNumber,
        //         Text = $"{v.Brand} {v.Type} [{v.LicenseNumber}]"
        //     });
    }
}