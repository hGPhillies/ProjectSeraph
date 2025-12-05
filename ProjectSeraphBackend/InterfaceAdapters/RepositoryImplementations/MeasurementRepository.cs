using ProjectSeraphBackend.Application.Interfaces;
using ProjectSeraphBackend.Domain;
using ProjectSeraphBackend.InterfaceAdapters.Interfaces;

namespace ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public class MeasurementRepository : IMeasurementRepository
    {
        private IMeasurementDAO _dao;

        public MeasurementRepository(IMeasurementDAO dao)
        {
            _dao = dao;
        }
        public async Task AddAsync(Measurement m)
        {
            await _dao.CreateAsync(m);
        }

        public async Task<Measurement> GetAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Measurement>> GetAllAsync(string citizenID)
        {
            return await _dao.ReadAllAsync(citizenID);
        }

        public async Task UpdateAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
