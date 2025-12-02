using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProjectSeraphBackend.Domain;
using ProjectSeraphBackend.InterfaceAdapters.Interfaces;

namespace ProjectSeraphBackend.FrameworksAndDrivers.DatabaseAccess
{
    public class MeasurementDAOMongo : IMeasurementDAO
    {
        private readonly IMongoCollection<Measurement> _measurements;

        public MeasurementDAOMongo(IMongoDatabase db)
        {
            _measurements = db.GetCollection<Measurement>("measurements");
        }

        public static void MapMeasurementMembers()
        {
            BsonClassMap.RegisterClassMap<Measurement>(cm =>
            {
                cm.MapIdMember(m => m.MeasurementID);
                cm.MapMember(m => m.CitizenID);
                cm.MapMember(m => m.Time);
                cm.SetIsRootClass(true);
            });
            BsonClassMap.RegisterClassMap<Bloodpressure>(cm =>
            {
                cm.MapMember(m => m.Systolic);
                cm.MapMember(m => m.Diastolic);
            });
            BsonClassMap.RegisterClassMap<Bloodsugar>(cm =>
            {
                cm.MapMember(m => m.millimolePerLiter);
            });
            BsonClassMap.RegisterClassMap<Pulse>(cm =>
            {
                cm.MapMember(m => m.BeatsPerMinute);
            });
        }

        public async Task CreateMeasurementAsync(Measurement measurement)
        {
            await _measurements.InsertOneAsync(measurement);
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
