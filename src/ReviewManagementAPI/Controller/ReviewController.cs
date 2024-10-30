using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReviewManagementAPI.Commands;
using ReviewManagementAPI.Models;
using ReviewManagementAPI.Services.Interfaces;

namespace ReviewManagementAPI.Models;

[ApiController]
[Route("/api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public Review CreateReview([FromBody] CreateReview command)
    {
        return _reviewService.AddReview(command);
    }

    [HttpGet]
    public List<Review> GetAllReviews()
    {
        return _reviewService.GetAllReviews();
    }

    [HttpGet("{id}")]
    public Review GetReviewById(string id)
    {
        return _reviewService.GetReviewById(id);
    }
    
    [HttpPost("{id}")]
    public void Delete(string id)
    {
        _reviewService.Delete(id);
    }
}