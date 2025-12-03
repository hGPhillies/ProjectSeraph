using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.Application.Interfaces
{
    /// <summary>
    /// Defines a repository interface defines the contract for nurse data operations.
    /// This interface provides asynchronous methods for CRUD operations on nurse entities, allowing implementations to interact with various data sources.
    /// </summary>
    
    public interface INurseRepository
    {
        Task<Nurse?> GetByIdAsync(string nurseID);
        Task<IEnumerable<Nurse>> GetAllAsync();
        Task<Nurse> CreateAsync(Nurse nurse);
        Task<Nurse> UpdateAsync(string nurseID, Nurse nurse);
        Task<bool> DeleteAsync(string nurseID);
    }
}
