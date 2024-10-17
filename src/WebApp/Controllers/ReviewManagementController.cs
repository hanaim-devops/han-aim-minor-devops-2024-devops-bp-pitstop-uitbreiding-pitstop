using System.Collections;
using PitStop.WebApp.Controllers;

namespace Pitstop.WebApp.Controllers;

public class ReviewManagementController : Controller
{
    //private IReviewManagementAPI _reviewManagementAPI;
    private ICustomerManagementAPI _customerManagementAPI;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public ReviewManagementController(/*IReviewManagementAPI reviewManagementAPI,*/ ICustomerManagementAPI customerManagementAPI, ILogger<ReviewManagementController> logger)
    {
        //_reviewManagementAPI = reviewManagementAPI;
        _customerManagementAPI = customerManagementAPI;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        Review review = new Review
        {
            ReviewId = "1",
            Customer = new Customer
            {
                CustomerId = "1",
                Name = "John Doe",
                EmailAddress = "hi@hi.nl"
            },
            Title = "Great service",
            Stars = 5
        };
        
        IEnumerable<Review> reviews = new List<Review> { review };
                    
        return await _resiliencyHelper.ExecuteResilient(async () =>
        {
            var model = new ReviewManagementViewModel
            {
                //Reviews = await _reviewManagementAPI.GetReviews()
                Reviews = reviews
            };
            return View(model);
        }, View("Offline", new ReviewManagementOfflineViewModel()));
    }
}