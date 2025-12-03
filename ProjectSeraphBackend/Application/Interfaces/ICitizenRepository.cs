using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.Application.Interfaces
{
    public interface ICitizenRepository
    {
        Task<Citizen?> GetByIdAsync(string citizenID);
        Task<IEnumerable<Citizen>> GetAllAsync();
        Task<Citizen> CreateAsync(Citizen citizen);
        Task<Citizen> UpdateAsync(string citizenID, Citizen citizen);
        Task<bool> DeleteAsync(string citizenID);
        Task<IEnumerable<Citizen>> GetByCityAsync(string city);
        Task<IEnumerable<Citizen>> GetByAgeRangeAsync(int minAge, int maxAge);
    }
}
