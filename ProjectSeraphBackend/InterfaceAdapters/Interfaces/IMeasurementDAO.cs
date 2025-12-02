using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.InterfaceAdapters.Interfaces
{
    //used to show how dependency on external database could be moved to Frameworks and Drivers layer
    public interface IMeasurementDAO
    {
        Task CreateMeasurementAsync(Measurement measurement);
        Task<Measurement> ReadMeasurementAsync(int measurementId);
        Task<IEnumerable<Measurement>> ReadAllMeasurementsAsync(int citizenId);
        Task UpdateMeasurementAsync(Measurement measurement);
        Task DeleteMeasurementAsync(Measurement measurement);
    }
}
