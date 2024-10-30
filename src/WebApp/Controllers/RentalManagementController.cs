namespace PitStop.WebApp.Controllers;

public class RentalManagementController : Controller
{
    private IRentalManagementAPI _rentalManagementAPI;
    private IRentalCarManagementAPI _rentalCarManagementAPI;
    private ICustomerManagementAPI _customerManagementAPI;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public RentalManagementController(IRentalManagementAPI rentalManagementAPI, IRentalCarManagementAPI rentalCarManagementAPI, ICustomerManagementAPI customerManagementAPI, ILogger<RentalManagementController> logger)
    {
        _rentalManagementAPI = rentalManagementAPI;
        _rentalCarManagementAPI = rentalCarManagementAPI;
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
                Rentals = new List<Rental>()
            };

            List<Rental> rentals = await _rentalManagementAPI.GetRentals();
            if (rentals?.Count > 0)
            {
                model.Rentals.AddRange(rentals.OrderBy(r => r.StartDate));
            }

            return View(model);
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(string rentalId)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new RentalManagementDetailsViewModel
            {
                Rental = await _rentalManagementAPI.GetRentalById(rentalId)
            };

            return View(model);
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> New()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            DateTime currentDate = DateTime.Today;
            var customers = await GetCustomersList();
            var availableRentalCars = await GetAvailableRentalCarsList();
            
            var model = new RentalManagementNewViewModel
            {
                StartDate = currentDate,
                EndDate = currentDate.AddDays(1),
                RentalCars = availableRentalCars,
                Customers = customers
            };
            return View(model);
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(string rentalId)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            Rental currentRental = await _rentalManagementAPI.GetRentalById(rentalId);
            
            var customers = await GetCustomersList();
            var availableRentalCars = await GetAvailableRentalCarsList();
            
            var model = new RentalManagementNewViewModel
            {
                Id = currentRental.Id,
                StartDate = currentRental.StartDate,
                EndDate = currentRental.EndDate,
                SelectedCustomerId = currentRental.Customer.CustomerId,
                SelectedRentalCarId = currentRental.Car.Id,
                RentalCars = availableRentalCars,
                Customers = customers
            };
            return View(model);
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCarRental([FromForm] RentalManagementNewViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                try
                {
                    RegisterRental registerRentalCommand = new RegisterRental(Guid.NewGuid(), inputModel.SelectedRentalCarId,
                        inputModel.SelectedCustomerId, inputModel.StartDate, inputModel.EndDate);
                    await _rentalManagementAPI.RegisterRental(registerRentalCommand);
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.Conflict)
                    {
                            // add errormessage from API exception to model
                            var content = await ex.GetContentAsAsync<BusinessRuleViolation>();
                        inputModel.Error = content.ErrorMessage;

                            // repopulate list of available vehicles in the model
                            inputModel.RentalCars = await GetAvailableRentalCarsList();
                            inputModel.Customers = await GetCustomersList();

                            // back to New view
                            return View("New", inputModel);
                    }
                }

                return RedirectToAction("Index");
            }, View("Offline", new RentalManagementOfflineViewModel()));
        }
        else
        {
            inputModel.RentalCars = await GetAvailableRentalCarsList();
            inputModel.Customers = await GetCustomersList();
            return View("New", inputModel);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateCarRental([FromForm] RentalManagementNewViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                try
                {
                    Rental currentRental = await _rentalManagementAPI.GetRentalById(inputModel.Id);
                    
                    if(inputModel.EndDate != currentRental.EndDate)
                    {
                        ExtendRental extendRentalCommand = new ExtendRental(Guid.NewGuid(), inputModel.EndDate);
                        await _rentalManagementAPI.ExtendRental(inputModel.Id, extendRentalCommand);
                    }
                    
                    if(inputModel.SelectedRentalCarId != currentRental.Car.Id)
                    {
                        ChangeCarRental changeCarRentalCommand = new ChangeCarRental(Guid.NewGuid(), inputModel.SelectedRentalCarId);
                        await _rentalManagementAPI.ChangeCarRental(inputModel.Id, changeCarRentalCommand);
                    }
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.Conflict)
                    {
                        // add errormessage from API exception to model
                        var content = await ex.GetContentAsAsync<BusinessRuleViolation>();
                        inputModel.Error = content.ErrorMessage;

                        // repopulate list of available vehicles in the model
                        inputModel.RentalCars = await GetAvailableRentalCarsList();
                        inputModel.Customers = await GetCustomersList();

                        // back to New view
                        return View("Edit", inputModel);
                    }
                }

                return RedirectToAction("Index");
            }, View("Offline", new RentalManagementOfflineViewModel()));
        }
        else
        {
            inputModel.RentalCars = await GetAvailableRentalCarsList();
            inputModel.Customers = await GetCustomersList();
            return View("Edit", inputModel);
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(string rentalId)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            await _rentalManagementAPI.DeleteRental(rentalId);
            
            return RedirectToAction("Index");
        }, View("Offline", new RentalManagementOfflineViewModel()));
    }
    
    private async Task<IEnumerable<SelectListItem>> GetAvailableRentalCarsList()
    {
        var vehicles = await _rentalCarManagementAPI.GetRentalCars();
        return vehicles.Select(v =>
            new SelectListItem
            {
                Value = v.Id,
                Text = $"{v.Model.Brand.Name} {v.Model.Name} [{v.LicenseNumber}]"
            });
    }
    
    private async Task<IEnumerable<SelectListItem>> GetCustomersList()
    {
        var customers = await _customerManagementAPI.GetCustomers();
        return customers.Select(c => new SelectListItem { Value = c.CustomerId, Text = c.Name });
    }
}