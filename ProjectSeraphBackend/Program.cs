using MongoDB.Driver;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.FrameworksAndDrivers.Endpoints;
using ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations;



namespace ProjectSeraphBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("MongoDb")
                                    ?? "mongodb://localhost:27017";
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("mongodb");

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
            builder.Services.AddSingleton<IMongoDatabase>(database);
            builder.Services.AddSingleton<IWebSocketService>();

            //Add Controllers
            //builder.Services.AddControllers().AddApplicationPart(typeof(FrameworksAndDrivers.Endpoints.CitizenEndpoints).Assembly);

            builder.Services.AddEndpointsApiExplorer();


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(Options =>
                {
                    Options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.MapControllers();

            app.MapAlarmWebSocket();

            app.Run();


        }
    }
}
