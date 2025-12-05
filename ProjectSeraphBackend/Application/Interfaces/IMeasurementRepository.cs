using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMeasurementRepository
    {
        Task AddAsync(Measurement measurement);
        Task<Measurement> GetAsync(Measurement measurement);
        Task<IEnumerable<Measurement>> GetAllAsync(string citizenID);
        Task UpdateAsync(Measurement measurement);
        Task DeleteAsync(Measurement measurement);
    }
}
