namespace ReviewManagmentAPI.Models;

public class Review
{
    public string Id { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public string ReviewerName { get; set; }
}