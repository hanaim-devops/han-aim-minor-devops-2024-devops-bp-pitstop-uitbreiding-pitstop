namespace Pitstop.WebApp.ViewModels;

public class ReviewManagementEditViewModel
{
    public string ReviewId { get; set; }
    public string ReviewerName { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; }

    public UpdateReview MapToUpdateReview()
    {
        return new UpdateReview
        {
            ReviewId = ReviewId,
            ReviewerName = ReviewerName,
            Rating = Rating,
            Content = Content
        };
    }
}