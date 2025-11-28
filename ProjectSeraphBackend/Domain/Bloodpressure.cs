

namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    ///This class represents a blood pressure measurement with systolic and diastolic values.
    /// </summary>
    public class Bloodpressure : Measurement
    {
        public double Systolic { get; set; }
        public double Diastolic { get; set; }
       
        public override bool CompareMeasurements() //Is this needed?
        {
            throw new NotSupportedException("Use CheckMeasurement to compare");
        }


        //public Bloodpressure(double systolic, double diastolic)
        //{
        //    this.systolic = systolic;
        //    this.diastolic = diastolic;
        //}

        //List<Bloodpressure> normalValues = new List<Bloodpressure>();

    }


}
