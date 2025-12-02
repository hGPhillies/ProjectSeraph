using ProjectSeraphBackend.Application.DTO;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;
using System.Runtime.CompilerServices;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{
    /// <summary>
    /// Represents a collection of endpoints used for retrieving or managing measurement data.
    /// </summary>
    /// <remarks>This class serves as a central point for accessing measurement-related operations. It can be
    /// used to interact with various measurement endpoints in a system.</remarks>
    public static class MeasurementEndpoints
    {
        public static IEndpointRouteBuilder MapMeasurementEndpoints(this IEndpointRouteBuilder measurements)
        {
            measurements.MapPost("/measurement/send/bloodpressure", async (Bloodpressure bp, IMeasurementRepository measRep) =>
            {

                await measRep.AddAsync(new Bloodpressure(bp));

                return Results.Created();
            })
            .WithName("SendBloodpressure");

            //measurements.MapPost("/measurement/send/bloodsugar", async (BloodsugarDTO bsDTO, IMeasurementRepository measRep) =>
            //{
            //    await measRep.Add(new Bloodsugar(bsDTO));

            //    return Results.Created();
            //})
            //.WithName("SendBloodsugar");

            //measurements.MapPost("/measurement/send/pulse", async (Pulse p, IMeasurementRepository measRep) =>
            //{
            //    await measRep.Add(new Pulse(p));

            //    return Results.Created();
            //})
            //.WithName("SendPulse");

            return measurements;
        }
    }
}
