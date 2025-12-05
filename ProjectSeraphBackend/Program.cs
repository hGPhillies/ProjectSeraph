using MongoDB.Driver;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Application.Services;
using ProjectSeraphBackend.FrameworksAndDrivers.DatabaseAccess;
using ProjectSeraphBackend.FrameworksAndDrivers.Endpoints;
using ProjectSeraphBackend.InterfaceAdapters.Interfaces;
using ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations;

namespace ProjectSeraphBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Register services first
            var connectionString = builder.Configuration.GetConnectionString("MongoDb")
                                    ?? "mongodb://localhost:27017";
            var databaseName = "mongodb";

            // Register MongoClient
            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(connectionString));

            // Register Logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            

            // Register IMongoDatabase
            builder.Services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(databaseName);
            });

            builder.Services.AddSingleton<IWebSocketService, WebSocketService>();
            //Maybe we can do the database mapping here?
            MeasurementDAOMongo.MapMeasurementMembers();
            builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            builder.Services.AddScoped<IMeasurementDAO, MeasurementDAOMongo>();
            
            //Repositories
            builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
            builder.Services.AddScoped<INurseRepository, NurseRepository>();

            //det foelgende bruger vi til at tillade cross-origin resource sharing under vores udviklingsproces. 
            //Dette er noedvendigt for at tillade vores frontend at kalde metoderne paa vores API fra en anden port
            const string MyAllowAnyOriginPolicy = "_myAllowAnyOriginPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowAnyOriginPolicy,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
                
                

            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseCors(MyAllowAnyOriginPolicy);

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
                });
            }

            app.UseHttpsRedirection();

            app.UseWebSockets();

            app.UseAuthorization();

            app.MapNurseEndpoints();
            app.MapCitizenEndpoints();
            app.MapAlarmWebSocket();
            app.MapMeasurementEndpoints();

            app.Run();

        }
    }
}