namespace Pitstop.WebApp.Commands;

public class UpdateReview
{
    public string ReviewId { get; set; }
    public string ReviewerName { get; set; }
    public int Rating { get; set; }
    public string Content { get; set; }
}