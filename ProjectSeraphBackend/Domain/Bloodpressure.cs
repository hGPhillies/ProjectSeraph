namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    ///     
    /// </summary>
    public class Bloodpressure
    {
        private int systolic;
        private int diastolic;
        public Bloodpressure(int systolic, int diastolic)
        {
            this.systolic = systolic;
            this.diastolic = diastolic;
        }

        List<Bloodpressure> normalValues = new List<Bloodpressure>();
    }
}
