using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{
    /// <summary>
    /// Provides API endpoints for managing citizens.
    /// This controller includes endpoints for retrieving, creating, updating, and deleting citizen records. 
    /// It interacts with the ICitizenRepository to perform data operations.
    /// </summary>
    
    public static class CitizenEndpoints
    {
        public static IEndpointRouteBuilder MapCitizenEndpoints(this IEndpointRouteBuilder app)
        {
            // Grouping all citizen-related endpoints under /citizen
            var citizenGroup = app.MapGroup("/citizen")
             .WithTags("CitizenEndpoints"); 

            //GET /citizen - endpoint to get all citizens
            citizenGroup.MapGet("/getAll", async (ICitizenRepository repo) =>
            {
                var citizens = await repo.GetAllAsync();
                return Results.Ok(citizens);
            })
                .WithName("GetAllCitizens");

            //GET /citizen/{citizenID} - endpoint to get a citizen by ID
            citizenGroup.MapGet("/{citizenID}", async (string citizenID, ICitizenRepository repo) =>
            {
                var citizen = await repo.GetByIdAsync(citizenID);
                return citizen is null ? Results.NotFound() : Results.Ok(citizen);
            })
                .WithName("GetCitizenById");

            //POST /citizen - endpoint to create a new citizen
            citizenGroup.MapPost("/", async (Citizen citizen, ICitizenRepository repo) =>
            {
                //citizen.citizenID = string.Empty; // Let repository generate ID
                var created = await repo.CreateAsync(citizen);
                return Results.Created($"/citizen/{created.citizenID}", created);
            })
                .WithName("CreateCitizen");

            //PUT /citizen/{citizenID} - endpoint to update a citizen
            citizenGroup.MapPut("/{citizenID}", async (string citizenID, Citizen citizen, ICitizenRepository repo) =>
            {
                var updated = await repo.UpdateAsync(citizenID, citizen);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            })
                .WithName("UpdateCitizen");

            //DELETE /citizen/{citizenID} - endpoint to delete a citizen by ID
            citizenGroup.MapDelete("/{citizenID}", async (string citizenID, ICitizenRepository repo) =>
            {
                var deleted = await repo.DeleteAsync(citizenID);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
                .WithName("DeleteCitizen");

            return app;
        }
    }
}
