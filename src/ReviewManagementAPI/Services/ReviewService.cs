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
            if (review != null)
            {
                return review;
            } 
            throw new Exception("Review not found");
        }
        
        public Review UpdateReview(string id, UpdateReview command)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.Id == id);
            if (review != null)
            {
                review.ReviewerName = command.ReviewerName;
                review.Rating = command.Rating;
                review.Content = command.Content;
                _dbContext.SaveChanges();
                return review;
            }
            throw new Exception("Review not found");
        }
    }
}