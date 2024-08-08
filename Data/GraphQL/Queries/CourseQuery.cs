using Data.Entites;
using Data.Services;

namespace Data.GraphQL.Queries;

public class CourseQuery(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("GetCourses")]
    public async Task<IEnumerable<CourseEntity>> GetCoursesAsync()
    {
        return await _courseService.GetCoursesAsync();
    }

    [GraphQLName("GetCourseById")]
    public async Task<CourseEntity> GetCourseByIdAsync(int id)
    {
        return await _courseService.GetCourseByIdAsync(id);
    }
}