
namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This abstract class represents a generic measurement with a timestamp.
    /// It defines an abstract method to compare measurements against normal values.
    /// </summary>

    public abstract class Measurement
    {
        //flg. 2 til at binde measurement til citizen.
        public int MeasurementID { get; set; }
        public int CitizenID { get; set; }
        // Timestamp of the measurement
        public DateTime Time { get; set; }

        public Measurement() { }
        public Measurement(int measurementID, int citizenID, DateTime time)
        {
            MeasurementID = measurementID;
            CitizenID = citizenID;
            Time = time;
        }


        //Returns true if measurement is within normal values, false otherwise
        public abstract bool CompareMeasurements();

    }
}
