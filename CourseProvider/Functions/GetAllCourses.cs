using Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseProvider.Functions
{
    public class GetAllCourses
    {
        private readonly ILogger<GetAllCourses> _logger;
        private readonly DataContext _dataContext;

        public GetAllCourses(ILogger<GetAllCourses> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [Function("GetAllCourses")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            try
            {
                var courses = await _dataContext.Courses.ToListAsync();
                return new OkObjectResult(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
