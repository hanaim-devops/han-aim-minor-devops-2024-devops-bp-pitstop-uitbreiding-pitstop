using System.Collections;
using PitStop.WebApp.Controllers;
using Pitstop.WebApp.RESTClients;

namespace Pitstop.WebApp.Controllers;

public class ReviewManagementController : Controller
{
    private IReviewManagementAPI _reviewManagementAPI;
    private ICustomerManagementAPI _customerManagementAPI;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public ReviewManagementController(IReviewManagementAPI reviewManagementAPI, ICustomerManagementAPI customerManagementAPI, ILogger<ReviewManagementController> logger)
    {
        _reviewManagementAPI = reviewManagementAPI;
        _customerManagementAPI = customerManagementAPI;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new ReviewManagementViewModel
            {
                Reviews = await _reviewManagementAPI.GetReviews()
            };
            return View(model);
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> New()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var customers = await _customerManagementAPI.GetCustomers();

            var model = new ReviewManagementNewViewModel
            {
                Customers = customers.Select(c => new SelectListItem { Value = c.Name, Text = c.Name })
            };
            return View(model);
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
    
    [HttpPost]
    public async Task<IActionResult> New([FromForm] ReviewManagementNewViewModel inputModel)
    {
        if (ModelState.IsValid)
        {
            return await _resiliencyHelper.ExecuteResilient(async () =>
            {
                try
                {
                    // Mapping the inputModel to a command that will be used for creating a new review.
                    CreateReview cmd = inputModel.MapToCreateReview();

                    // Call the Review API (backend service) to register the review.
                    await _reviewManagementAPI
                        .CreateReview(cmd); // Uncomment and implement this call when backend is ready.

                    // Redirect to the Index view upon successful creation.
                    return RedirectToAction("Index");
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.Conflict)
                    {
                        return Conflict();
                    }
                }

                // Default fallback: redirect to Index if no conflict or error occurs.
                return RedirectToAction("Index");
            }, View("Offline", new ReviewManagementOfflineViewModel()));
        } else
        {
            // If the input model is invalid, return to the New view with the current model (validation errors).
            return View("New", inputModel);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] string id) 
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            try
            {
                
                return RedirectToAction("Index");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return NotFound("Review not found.");
                else if (ex.StatusCode == HttpStatusCode.Conflict)
                    return Conflict("Unable to delete review due to a conflict.");
            }

            return RedirectToAction("Index");
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
    
}