namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This abstract class represents a generic measurement with a timestamp.
    /// It defines an abstract method to compare measurements against normal values.
    /// </summary>

    public abstract class Measurement
    {
        // Timestamp of the measurement
        public DateTime Time { get; set; }

        //Returns true if measurement is within normal values, false otherwise
        public abstract bool CompareMeasurements();
    }
}
