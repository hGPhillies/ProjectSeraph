namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This class represents a blood sugar measurement in millimoles per liter.
    /// </summary>
    public class Bloodsugar : Measurement
    {  
        public double millimolePerLiter { get; set; }

        public Bloodsugar() { }
        public Bloodsugar(string? measurementID, string citizenID, DateTime time, double mmPL) : base(measurementID, citizenID, time)
        {
            this.millimolePerLiter = mmPL;
        }

        public override bool CompareMeasurements() //Is this needed?
        {
            throw new NotSupportedException("Use CheckMeasurement to compare");
        }

        //I stedet for List giver det mening at bruge NormalLow og NormalHigh
        
        //List<Bloodsugar> normalValues = new List<Bloodsugar>();
    }
}
