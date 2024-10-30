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
            ReviewId = this.ReviewId,
            ReviewerName = this.ReviewerName,
            Rating = this.Rating,
            Content = this.Content
        };
    }
}