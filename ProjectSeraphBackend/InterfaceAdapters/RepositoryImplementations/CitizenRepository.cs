using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProjectSeraphBackend.Domain;
using ProjectSeraphBackend.Application.Interfaces;

namespace ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations
{
    public class CitizenRepository : ICitizenRepository
    {

        // Repository Implementation for Citizen Entity
        private readonly IMongoCollection<Citizen> _citizens;

        // Constructor to initialize MongoDB collection
        public CitizenRepository(IMongoDatabase db)
        {
            _citizens = db.GetCollection<Citizen>("Citizens");
        }


        
        public async Task<Citizen?> GetByIdAsync(string citizenID)
        {
            return await _citizens.Find(c => c.citizenID == citizenID).FirstOrDefaultAsync();
        }



        public async Task<IEnumerable<Citizen>> GetAllAsync()
        {
            return await _citizens.Find(_ => true).ToListAsync();
        }



        public async Task<Citizen> CreateAsync(Citizen citizen)
        {
            if (string.IsNullOrEmpty(citizen.citizenID))
            {
                citizen.citizenID = Guid.NewGuid().ToString();
            }

            await _citizens.InsertOneAsync(citizen);
            return citizen;
        }



        public async Task<Citizen> UpdateAsync(string citizenID, Citizen citizen)
        {
            var filter = Builders<Citizen>.Filter.Eq(c => c.citizenID, citizenID);
            var options = new FindOneAndReplaceOptions<Citizen>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await _citizens.FindOneAndReplaceAsync(filter, citizen, options);
        }



        public async Task<bool> DeleteAsync(string citizenID)
        {
            var result = await _citizens.DeleteOneAsync(c => c.citizenID == citizenID);
            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Citizen>> GetByCityAsync(string city)
        {
            return await _citizens.Find(c => c.home.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Citizen>> GetByAgeRangeAsync(int minAge, int maxAge)
        {
            return await _citizens.Find(c => c.age >= minAge && c.age <= maxAge).ToListAsync();
        }
    }
}



    

