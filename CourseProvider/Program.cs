using Data.Contexts;
using Data.GraphQL.Mutations;
using Data.GraphQL.ObjectTypes;
using Data.GraphQL.Queries;
using Data.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddPooledDbContextFactory<DataContext>(x => x.UseSqlServer(context.Configuration.GetConnectionString("SqlServer")));

        services.AddScoped<ICourseService, CourseService>();

        services.AddGraphQLFunction()
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


        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var dbcontext = dbContextFactory.CreateDbContext();
        dbcontext.Database.EnsureCreated();


    })
    .Build();

host.Run();