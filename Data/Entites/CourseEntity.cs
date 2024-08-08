namespace Data.Entites;

public class CourseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public decimal Price { get; set; }
    public int HoursToComplete { get; set; }
    public int LikesPercentage { get; set; }
    public int LikesAmount { get; set; }
    public string? Image { get; set; } = "/images/icons/no-profile-picture.svg";


    public string? AuthorName { get; set; } = "John Doe";
    public string? AuthorDescription { get; set; } = "An author with no description??";
    public string? AuthorImage { get; set; } = "/images/icons/no-profile-picture.svg";


    public int AuthorYoutubeFollowersQty { get; set; }
    public int AuthorFacebookFollowersQty { get; set; }
}