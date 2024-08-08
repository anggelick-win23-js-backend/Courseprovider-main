using CourseProvider.Models;
using Data.Contexts;
using Data.Entites;
using Data.Factories;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public interface ICourseService
{
    Task<CourseEntity> CreateCourseAsync(CourseCreateRequest request);
    Task<CourseEntity> GetCourseByIdAsync(int id);
    Task<List<CourseEntity>> GetCoursesAsync();
    Task<CourseEntity> UpdateCourseAsync(CourseUpdateRequest request);
    Task<bool> DeleteCourseAsync(int id);
}

public class CourseService : ICourseService
{
    //private readonly DataContext _context = context;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public CourseService(IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }


    public async Task<CourseEntity> CreateCourseAsync(CourseCreateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();

        var courseEntity = CourseFactory.CreateCourse(request);
        context.Courses.Add(courseEntity);
        await context.SaveChangesAsync();

        return courseEntity;
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var coursEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (coursEntity != null)
        {
            context.Courses.Remove(coursEntity);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<CourseEntity> GetCourseByIdAsync(int id)
    {
        await using var context = _contextFactory.CreateDbContext();

        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {
            return course;
        }

        return null!;
    }

    public async Task<List<CourseEntity>> GetCoursesAsync()
    {
        await using var context = _contextFactory.CreateDbContext();

        var courses = await context.Courses.ToListAsync(); 

        if (courses != null)
        {
            return courses;
        }
        else
        {
            return null!;
        }
    }

    public async Task<CourseEntity> UpdateCourseAsync(CourseUpdateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();
        var existingCourse = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (existingCourse != null)
        {
            var updatedCourse = CourseFactory.UpdateCourse(request);

            // unnecessary?
            updatedCourse.Id = request.Id;

            context.Entry(existingCourse).CurrentValues.SetValues(updatedCourse);
            await context.SaveChangesAsync();

            return updatedCourse;
        }
        return null!;
    }
}
