
using MongoDB.Driver;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations;

namespace ProjectSeraphBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

           
            var mongoClient = new MongoClient(builder.Configuration["Mongo:ConnectionString"]);
            var mongoDb = mongoClient.GetDatabase(builder.Configuration["Mongo:Database"]);
            builder.Services.AddSingleton<IMongoDatabase>(mongoDb);
            builder.Services.AddScoped<ICitizenRepository, CitizenRepository>();
        }
    }
}
