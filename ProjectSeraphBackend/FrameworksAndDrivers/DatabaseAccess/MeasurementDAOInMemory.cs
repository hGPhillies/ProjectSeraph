using ProjectSeraphBackend.Domain;
using ProjectSeraphBackend.InterfaceAdapters.Interfaces;

namespace ProjectSeraphBackend.FrameworksAndDrivers.DatabaseAccess
{
    public class MeasurementDAOInMemory : IMeasurementDAO
    {
        public async Task CreateMeasurementAsync(Measurement m)
        {
            Console.WriteLine("" + m.ToString());
        }

        public async Task<Measurement> ReadMeasurementAsync(int measurementId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Measurement>> ReadAllMeasurementsAsync(int citizenId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMeasurementAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMeasurementAsync(Measurement measurement)
        {
            throw new NotImplementedException();
        }
    }
}
