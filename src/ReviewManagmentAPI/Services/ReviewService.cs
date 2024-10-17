using ReviewManagmentAPI.Commands;
using ReviewManagmentAPI.DataAccess;
using ReviewManagmentAPI.Models;
using ReviewManagmentAPI.Services.Interfaces;

namespace ReviewManagmentAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewManagementDBContext _dbContext;

        public ReviewService(ReviewManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Review AddReview(CreateReview command)
        {
            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                Content = command.Content,
                Rating = command.Rating,
                ReviewerName = command.ReviewerName,
            };

            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();
            return review;
        }

        public List<Review> GetAllReviews()
        {
            return _dbContext.Reviews.ToList();
        }

        public Review GetReviewById(string id)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null) return review;
            throw new Exception("Review not found");
        }
    }
}