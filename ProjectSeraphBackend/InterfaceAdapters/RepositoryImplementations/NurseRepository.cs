using MongoDB.Driver;
using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations
{
    /// <summary>
    /// Provides methods for managing nurse records in a MongoDB database.
    /// </summary>
    /// <remarks>This repository offers asynchronous operations to create, retrieve, update, and delete nurse
    /// records. It interacts with a MongoDB collection to perform these operations.</remarks>

    public class NurseRepository : INurseRepository
    {
        // MongoDB collection for nurses
        public readonly IMongoCollection<Nurse> _nurses;

        // Constructor to initialize MongoDB collection
        public NurseRepository(IMongoDatabase db)
        {
            _nurses = db.GetCollection<Nurse>("nurses");
        }
     
        // Asynchronously retrieves a nurse by their unique identifier. 
        public async Task<Nurse?> GetByIdAsync(string nurseID)
        {
            return await _nurses.Find(n => n.nurseID == nurseID).FirstOrDefaultAsync();
        }

        // Asynchronously retrieves all nurses from the database
        public async Task<IEnumerable<Nurse>> GetAllAsync()
        {
            return await _nurses.Find(_ => true).ToListAsync();
        }

        // Asynchronously creates a new nurse record in the database
        public async Task<Nurse> CreateAsync(Nurse nurse)
        {
            nurse.nurseID = null; 
            await _nurses.InsertOneAsync(nurse);
            return nurse;
        }

        // Asynchronously updates an existing nurse record in the database
        public async Task<Nurse> UpdateAsync(string nurseID, Nurse nurse)
        {
            var filter = Builders<Nurse>.Filter.Eq(n => n.nurseID, nurseID);
            var options = new FindOneAndReplaceOptions<Nurse>
            {
                ReturnDocument = ReturnDocument.After
            };
            return await _nurses.FindOneAndReplaceAsync(filter, nurse, options);
        }

        // Asynchronously deletes a nurse record from the database by their unique identifier
        public async Task<bool> DeleteAsync(string nurseID)
        {
            var result = await _nurses.DeleteOneAsync(n => n.nurseID == nurseID);
            return result.DeletedCount > 0;
        }
    }
}
