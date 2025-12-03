using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.InterfaceAdapters.Interfaces
{
    //used to show how dependency on external database could be moved to Frameworks and Drivers layer
    public interface IMeasurementDAO
    {
        Task CreateAsync(Measurement measurement);
        Task<Measurement> ReadAsync(int measurementId);
        Task<IEnumerable<Measurement>> ReadAllAsync(int citizenId);
        Task UpdateAsync(Measurement measurement);
        Task DeleteAsync(Measurement measurement);
    }
}
