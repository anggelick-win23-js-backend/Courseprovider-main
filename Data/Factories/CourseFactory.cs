using Data.Entites;
using Data.Models;

namespace Data.Factories;

public static class CourseFactory
{
    public static CourseEntity CreateCourse(CourseCreateRequest request)
    {
        return new CourseEntity
        {
            Name = request.Name!,
            Description = request.Description!,
            Ingress = request.Ingress!,
            Price = request.Price,
            HoursToComplete = request.HoursToComplete,
            LikesPercentage = request.LikesPercentage,
            LikesAmount = request.LikesAmount,
            Image = request.Image,
            AuthorName = request.AuthorName,
            AuthorDescription = request.AuthorDescription,
            AuthorImage = request.AuthorImage,
            AuthorYoutubeFollowersQty = request.AuthorYoutubeFollowersQty,
            AuthorFacebookFollowersQty = request.AuthorFacebookFollowersQty
        };
    }

    public static CourseEntity UpdateCourse(CourseUpdateRequest request)
    {
        return new CourseEntity
        {
            Id = request.Id,
            Name = request.Name!,
            Description = request.Description!,
            Ingress = request.Ingress!,
            Price = request.Price,
            HoursToComplete = request.HoursToComplete,
            LikesPercentage = request.LikesPercentage,
            LikesAmount = request.LikesAmount,
            Image = request.Image,
            AuthorName = request.AuthorName,
            AuthorDescription = request.AuthorDescription,
            AuthorImage = request.AuthorImage,
            AuthorYoutubeFollowersQty = request.AuthorYoutubeFollowersQty,
            AuthorFacebookFollowersQty = request.AuthorFacebookFollowersQty
        };
    }

    public static Course CreateCourse(CourseEntity entity)
    {
        return new Course
        {
            Name = entity.Name!,
            Description = entity.Description!,
            Ingress = entity.Ingress!,
            Price = entity.Price,
            HoursToComplete = entity.HoursToComplete,
            LikesPercentage = entity.LikesPercentage,
            LikesAmount = entity.LikesAmount,
            Image = entity.Image,
            AuthorName = entity.AuthorName,
            AuthorDescription = entity.AuthorDescription,
            AuthorImage = entity.AuthorImage,
            AuthorYoutubeFollowersQty = entity.AuthorYoutubeFollowersQty,
            AuthorFacebookFollowersQty = entity.AuthorFacebookFollowersQty
        };
    }
}