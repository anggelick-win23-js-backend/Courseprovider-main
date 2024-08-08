using CourseProvider.RequestModels;
using Data.Contexts;
using Data.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CourseProvider.Functions
{
    public class GetOneCourse
    {
        private readonly ILogger<GetOneCourse> _logger;
        private readonly DataContext _dataContext;

        public GetOneCourse(ILogger<GetOneCourse> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [Function("GetOneCourse")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();

            if (body != null)
            {
                try
                {
                    var cr = JsonConvert.DeserializeObject<CourseRequest>(body);

                    var content = Convert.ToInt32(cr!.Id);

                    CourseEntity course = new();

                    course = _dataContext.Courses.FirstOrDefault(x => x.Id == content)!;

                    return new OkObjectResult(course);
                }
                catch (Exception ex) { }
            }
            return new BadRequestResult();
        }
    }
}
