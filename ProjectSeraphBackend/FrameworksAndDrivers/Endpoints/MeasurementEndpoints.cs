using ProjectSeraphBackend.Application.DTO;
using ProjectSeraphBackend.Application.Interfaces;
using System.Runtime.CompilerServices;

namespace ProjectSeraphBackend.FrameworksAndDrivers.Endpoints
{
    /// <summary>
    /// Represents a collection of endpoints used for retrieving or managing measurement data.
    /// </summary>
    /// <remarks>This class serves as a central point for accessing measurement-related operations. It can be
    /// used to interact with various measurement endpoints in a system.</remarks>
    public class MeasurementEndpoints
    {
        public static IEndpointRouteBuilder MapMeasurementEndpoints(this IEndpointRouteBuilder measurements)
        {
            measurements.MapPost("/measurement/send/bloodpressure", async (BloodpressureDTO bpDTO, IMeasurementRepository measRep) =>
            {
                await measRep.Add(new Measurement(measDTO));

                return Results.Created();
            })
            .WithName("SendBloodpressure");

            measurements.MapPost("/measurement/send/bloodsugar", async (BloodsugarDTO bsDTO, IMeasurementRepository measRep) =>
            {
                await measRep.Add(new Measurement(measDTO));

                return Results.Created();
            })
            .WithName("SendBloodsugar");

            measurements.MapPost("/measurement/send/pulse", async (PulseDTO pDTO, IMeasurementRepository measRep) =>
            {
                await measRep.Add(new Measurement(measDTO));

                return Results.Created();
            })
            .WithName("SendPulse");

            return measurements;
        }
    }
}
