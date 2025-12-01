

namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    ///This class represents a blood pressure measurement with systolic and diastolic values.
    /// </summary>
    public class Bloodpressure : Measurement
    {
        public double Systolic { get; set; }
        public double Diastolic { get; set; }

        public Bloodpressure()
        {

        }

        //REFACTOR: make fluent builderpattern for all measurements? Maybe mbetter with factory, as measurement can be three different ones.
        public Bloodpressure(int measurementID, int citizenID, DateTime time, double systolic, double diastolic) : base(measurementID, citizenID, time)
        {
            this.Systolic = systolic;
            this.Diastolic = diastolic;
        }
       
        public override bool CompareMeasurements() //Is this needed?
        {
            throw new NotSupportedException("Use CheckMeasurement to compare");
        }



        //List<Bloodpressure> normalValues = new List<Bloodpressure>();

    }


}
