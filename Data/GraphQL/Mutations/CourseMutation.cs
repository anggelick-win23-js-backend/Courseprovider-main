using Data.Entites;
using Data.Models;
using Data.Services;

namespace Data.GraphQL.Mutations;

public class CourseMutation
{
    private readonly ICourseService _courseService;

    public CourseMutation(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [GraphQLName("createCourse")]
    public async Task<CourseEntity> CreateCourseAsync(CourseCreateRequest input)
    {
        return await _courseService.CreateCourseAsync(input);
    }

    [GraphQLName("updateCourse")]
    public async Task<CourseEntity> UpdateCourseAsync(CourseUpdateRequest input)
    {
        return await _courseService.UpdateCourseAsync(input);
    }

    [GraphQLName("deleteCourse")]
    public async Task<bool> DeleteCourseAsync(int id)
    {
        return await _courseService.DeleteCourseAsync(id);
    }
}