namespace WebApp.RESTClients;

public interface IReviewManagementAPI
{
    [Get("/reviews")]
    Task<List<Review>> GetReviews();

    [Get("/reviews/{id}")]
    Task<Review> GetReviewById([AliasAs("id")] string reviewId);

    [Post("/reviews")]
    Task CreateReview(CreateReview command);
    
    [Put("/reviews/{id}")]
    Task UpdateReview([AliasAs("id")] string reviewId, UpdateReview command);
}