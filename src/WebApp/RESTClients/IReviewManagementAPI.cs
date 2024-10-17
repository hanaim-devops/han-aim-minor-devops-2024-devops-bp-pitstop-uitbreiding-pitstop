namespace Pitstop.WebApp.RESTClients;

public interface IReviewManagementAPI
{
    [Get("/reviews")]
    Task<List<Review>> GetReviews();

    [Get("/review/{id}")]
    Task<Review> GetReviewById([AliasAs("id")] string licenseNumber);

    [Post("/review")]
    Task CreateReview(CreateReview command);
}