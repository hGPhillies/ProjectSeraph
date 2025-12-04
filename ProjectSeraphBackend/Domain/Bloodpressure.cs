

namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    ///This class represents a blood pressure measurement with systolic and diastolic values.
    /// </summary>
    public class Bloodpressure : Measurement
    {
        private Bloodpressure bp;

        public double Systolic { get; set; }
        public double Diastolic { get; set; }
        public double Pulse { get; set; }

        public Bloodpressure()
        {

        }

        //REFACTOR: make fluent builderpattern for all measurements? Maybe mbetter with factory, as measurement can be three different ones.
        public Bloodpressure(string? measurementID, int citizenID, DateTime time, double systolic, double diastolic, double pulse) : base(measurementID, citizenID, time)
        {
            this.Systolic = systolic;
            this.Diastolic = diastolic;
            this.Pulse = pulse;
        }

        public Bloodpressure(Bloodpressure bp)
        {
            this.bp = bp;
        }

        public override bool CompareMeasurements() //Is this needed?
        {
            throw new NotSupportedException("Use CheckMeasurement to compare");
        }



        //List<Bloodpressure> normalValues = new List<Bloodpressure>();

        //DELETE THIS LATER
        public override string ToString()
        {
            return "" + this.MeasurementID.ToString() + this.CitizenID.ToString() + this.Time.ToString() + this.Diastolic + this.Systolic;
        }
    }


}
