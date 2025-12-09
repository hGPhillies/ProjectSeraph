using ProjectSeraphBackend.Domain;
using ProjectSeraphBackend.InterfaceAdapters.Interfaces;

namespace ProjectSeraphBackend.FrameworksAndDrivers.DatabaseAccess
{
    public class MeasurementDAOInMemory : IMeasurementDAO
    {
        public async Task CreateAsync(Measurement m)
        {
            Console.WriteLine("mID " + m.MeasurementID + "\ncID " + m.CitizenID + "\nDateTime " + m.Time);
        }

        public async Task<Measurement> ReadAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Measurement>> ReadAllByCitIDAsync(string citizenId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Measurement>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
