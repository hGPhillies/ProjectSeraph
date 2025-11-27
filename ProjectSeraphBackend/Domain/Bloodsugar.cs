namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Bloodsugar
    {
        private double millimolePerLiter;
        private Bloodsugar(double bloodsugarLevel)
        {
            this.millimolePerLiter = bloodsugarLevel;
        }

        List<Bloodsugar> normalValues = new List<Bloodsugar>();

    }
}
