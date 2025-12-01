using MongoDB.Bson.Serialization;
using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.InterfaceAdapters.RepositoryImplementations
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    public class MeasurementRepositoryMongo
    {
        //REFACTOR ? maybe move this to some MONGO schema or configuration class?
        public MeasurementRepositoryMongo() 
        {
            BsonClassMap.RegisterClassMap<Measurement>( cm =>
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
        public void getMeasurements()
        {
        }
        public void postMeasurement()
        {
        }
    }
}
