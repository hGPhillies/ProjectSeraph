namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This class represents a pulse measurement in beats per minute.
    /// </summary>
    public class Pulse : Measurement
    {
        public double BeatsPerMinute { get; set; }

        public Pulse()
        {

        }
        public Pulse(double BeatsPerMinute)
        {
            this.BeatsPerMinute = BeatsPerMinute;
        }
      
        public override bool CompareMeasurements() //Is this needed?
        {
           throw new NotSupportedException("Use CheckMeasurement to compare");
        }
    }
}
