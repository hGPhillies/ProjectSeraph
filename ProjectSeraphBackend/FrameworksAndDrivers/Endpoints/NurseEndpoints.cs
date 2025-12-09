using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{
    /// <summary>
    /// Provides RESTful API endpoints for managing nurses, including creating,
    /// retrieving, updating, and deleting nurse records. Uses INurseRepository
    /// for all data operations.
    /// </summary>

    public static class NurseEndpoints
    {
        //Routebuilder extension method to map nurse endpoints
        public static IEndpointRouteBuilder MapNurseEndpoints(this IEndpointRouteBuilder app)
        {
            // Grouping all nurse-related endpoints under /nurse
            var nurseGroup = app.MapGroup("/nurse")
            .WithTags("NurseEndpoints"); 

            //GET /nurses - endpoint to get all nurses
            nurseGroup.MapGet("/getAll", async (INurseRepository repo) =>
            {
                var nurses = await repo.GetAllAsync();
                return Results.Ok(nurses);
            })
                .WithName("GetAllNurses");

            //GET /nurses{id} - endpoint to get a nurse by ID
            nurseGroup.MapGet("/{nurseID}", async (string nurseID, INurseRepository repo) =>
            {
                var nurse = await repo.GetByIdAsync(nurseID);
                return nurse is null ? Results.NotFound() : Results.Ok(nurse);
            })
                .WithName("GetNurseById");

            //POST /nurses - endpoint to create a new nurse
            nurseGroup.MapPost("/", async (Nurse nurse, INurseRepository repo) =>
            {
                nurse.nurseID = string.Empty; // Let repository generate ID
                var created = await repo.CreateAsync(nurse);
                return Results.Created($"/api/nurses/{created.nurseID}", created);
            })
                .WithName("CreateNurse");

            //PUT /nurses/{nurseID} - endpoint to update an existing nurse
            nurseGroup.MapPut("/{nurseID}", async (string nurseID, Nurse nurse, INurseRepository repo) =>
            {
                var updated = await repo.UpdateAsync(nurseID, nurse);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            })
                .WithName("UpdateNurse");

            //DELETE /nurses/{nurseID} - endpoint to delete a nurse by ID
            nurseGroup.MapDelete("/{nurseID}", async (string nurseID, INurseRepository repo) =>
            {
                var deleted = await repo.DeleteAsync(nurseID);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
                .WithName("DeleteNurse");

            return app;
        }
    }
}
