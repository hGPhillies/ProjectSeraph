
using MongoDB.Driver;

namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This abstract class represents a generic measurement with a timestamp.
    /// It defines an abstract method to compare measurements against normal values.
    /// </summary>

    public abstract class Measurement
    {
        public string? MeasurementID { get; set; }
        //flg. til at binde measurement til citizen.
        public string CitizenID { get; set; }
        // Timestamp of the measurement
        public DateTime Time { get; set; }

        public Measurement() { }
        public Measurement(string? measurementID, string citizenID, DateTime time)
        {
            MeasurementID = measurementID;
            CitizenID = citizenID;
            Time = time;
        }


        //Returns true if measurement is within normal values, false otherwise
        public abstract bool CompareMeasurements();

    }
}
