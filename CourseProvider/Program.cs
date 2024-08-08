using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Services;
using Data.GraphQL.Queries;
using Data.GraphQL.Mutations;
using Data.GraphQL.ObjectTypes;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddPooledDbContextFactory<DataContext>(x => x.UseSqlServer(context.Configuration.GetConnectionString("SqlServer")));
        services.AddScoped<ICourseService, CourseService>();

        services.AddGraphQLServer()
            .AddQueryType<CourseQuery>()
            .AddMutationType<CourseMutation>()
            .AddType<CourseType>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });
    })
    .Build();

host.Run();

