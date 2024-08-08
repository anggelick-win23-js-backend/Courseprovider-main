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
using Azure.Storage.Blobs;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        // Add Application Insights telemetry
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Configure DbContext with SQL Server
        services.AddPooledDbContextFactory<DataContext>(x =>
            x.UseSqlServer(context.Configuration.GetConnectionString("SqlServer")));

        // Register application services
        services.AddScoped<ICourseService, CourseService>();

        // Configure GraphQL
        services.AddGraphQLFunction()
            .AddQueryType<CourseQuery>()
            .AddMutationType<CourseMutation>()
            .AddType<CourseType>();

        // Configure CORS
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Register BlobServiceClient
        services.AddSingleton(s =>
        {
            var connectionString = context.Configuration.GetConnectionString("VaultUri");
            return new BlobServiceClient(connectionString);
        });

        // Ensure the database is created
        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var dbcontext = dbContextFactory.CreateDbContext();
        dbcontext.Database.EnsureCreated();
    })
    .Build();

host.Run();
