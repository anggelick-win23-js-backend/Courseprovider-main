using Data.Contexts;
using Data.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CourseProvider.Functions
{
    public class CreateCourse
    {
        private readonly ILogger<CreateCourse> _logger;
        private readonly DataContext _dataContext;

        public CreateCourse(ILogger<CreateCourse> logger)
        {
            _logger = logger;
        }

        [Function("CreateCourse")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var courseEntity = JsonConvert.DeserializeObject<CourseEntity>(body);

            if (courseEntity != null)
            {
                var result = await _dataContext.Courses.AddAsync(courseEntity);
                await _dataContext.SaveChangesAsync();

                if (result.IsKeySet) 
                {
                    return new OkObjectResult(result);
                }
            }
            return new BadRequestResult();
        }
    }
}