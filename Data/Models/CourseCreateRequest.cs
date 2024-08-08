namespace Data.Models;

public class CourseCreateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; } 
    public string? Ingress { get; set; } 
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int LikesPercentage { get; set; }
    public int LikesAmount { get; set; }
    public string? Image { get; set; } 


    public string? AuthorName { get; set; }
    public string? AuthorDescription { get; set; } 
    public string? AuthorImage { get; set; } 


    public int AuthorYoutubeFollowersQty { get; set; }
    public int AuthorFacebookFollowersQty { get; set; }

}
