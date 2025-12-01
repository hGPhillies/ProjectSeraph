using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.InterfaceAdapters.Interfaces
{
    //used to show how dependency on external database could be moved to Frameworks and Drivers layer
    public interface IMeasurementDAO
    {
        void CreateMeasurement(Measurement measurement);
        Measurement ReadMeasurement(int measurementId);
        void UpdateMeasurement(Measurement measurement);
        void DeleteMeasurement(int measurementId);
    }
}
