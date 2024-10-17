using ReviewManagmentAPI.Commands;
using ReviewManagmentAPI.Models;

namespace ReviewManagmentAPI.Services.Interfaces;

public interface IReviewService
{
    Review AddReview(CreateReview command);
    List<Review> GetAllReviews();
    Review GetReviewById(string id);
}