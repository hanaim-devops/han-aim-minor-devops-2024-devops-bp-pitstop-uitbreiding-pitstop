using System;
using System.Collections.Generic;
using System.Linq;
using ReviewManagementAPI.Commands;
using ReviewManagementAPI.DataAccess;
using ReviewManagementAPI.Models;
using ReviewManagementAPI.Services.Interfaces;

namespace ReviewManagementAPI.Services
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


        public void DeleteReview(string reviewId)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review != null)
            {
                _dbContext.Reviews.Remove(review);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Review not found");
            }
        }
    }
}