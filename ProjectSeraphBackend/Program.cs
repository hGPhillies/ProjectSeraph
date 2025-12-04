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

            //Register services without controllers first
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

            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapNurseEndpoints();

            app.MapAlarmWebSocket();

            app.MapMeasurementEndpoints();

            app.Run();


            //USED WHEN ADDING CONTROLLERS INSTEAD OF ENDPOINTS

            //// Register Citizen repository with factory
            //builder.Services.AddScoped<ICitizenRepository>(sp =>
            //{
            //    var database = sp.GetRequiredService<IMongoDatabase>();
            //    return new CitizenRepository(database);
            //});

            //// Register Nurse repository with factory
            //builder.Services.AddScoped<INurseRepository>(sp =>
            //{
            //    var database = sp.GetRequiredService<IMongoDatabase>();
            //    return new NurseRepository(database);
            //});

            ////Add controllers without specifying assembly
            //builder.Services.AddControllers();

            //builder.Services.AddEndpointsApiExplorer();  

            //app.MapControllers();
        }
    }
}