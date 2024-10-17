namespace ReviewManagmentAPI.Commands;

public class CreateReview
{
    public string Content { get; set; }
    public int Rating { get; set; }
    public string ReviewerName { get; set; }
}