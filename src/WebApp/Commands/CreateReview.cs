namespace Pitstop.WebApp.Commands;

public class CreateReview
{
    public readonly string ReviewerName;
    public readonly string Content;
    public readonly int Rating;

    public CreateReview(string reviewerName, string content, int rating)
    {
        ReviewerName = reviewerName;
        Content = content;
        Rating = rating;
    }
}