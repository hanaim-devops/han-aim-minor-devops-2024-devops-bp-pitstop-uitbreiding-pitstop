namespace PitStop.WebApp.Controllers;

public class ReviewManagementController : Controller
{
    private IReviewManagementAPI _reviewManagementApi;
    private ICustomerManagementAPI _customerManagementApi;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public ReviewManagementController(IReviewManagementAPI reviewManagementApi, ICustomerManagementAPI customerManagementAPI, ILogger<ReviewManagementController> logger)
    {
        _reviewManagementApi = reviewManagementApi;
        _customerManagementApi = customerManagementAPI;
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
                Reviews = await _reviewManagementApi.GetReviews()
            };
            
            return View(model);
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
    
    [HttpGet]
    public async Task<IActionResult> New()
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var customers = await _customerManagementApi.GetCustomers();

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
                    await _reviewManagementApi
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
    public async Task<IActionResult> Edit(string id)
    {
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var review = await _reviewManagementApi.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            var model = new ReviewManagementEditViewModel
            {
                ReviewId = review.Id,
                ReviewerName = review.ReviewerName,
                Rating = review.Rating,
                Content = review.Content
            };

            return View(model);
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] ReviewManagementEditViewModel inputModel)
    {
        // If the input model is invalid, return to the Edit view with the current model (validation errors).
        if (!ModelState.IsValid)
        {
            return View("Edit", inputModel);
        }

        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            try {
                // Mapping the inputModel to a command that will be used for updating the review.
                var cmd = inputModel.MapToUpdateReview();

                // Call the Review API (backend service) to update the review.
                await _reviewManagementApi.UpdateReview(inputModel.ReviewId, cmd);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.Conflict)
                {
                    return Conflict();
                }
            }
            
            return RedirectToAction("Index");
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
}