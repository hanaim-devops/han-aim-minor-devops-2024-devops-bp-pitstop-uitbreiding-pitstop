namespace Pitstop.WebApp.Models;

public class Review
{
    public string ReviewId { get; set; }
    
    [Display(Name = "Reviewer")]
    public Customer Customer { get; set; }
    
    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; }
    
    [Required]
    [Display(Name = "Stars")]
    public int Stars { get; set; }
}