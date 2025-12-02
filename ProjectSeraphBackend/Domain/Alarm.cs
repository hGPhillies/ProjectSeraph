namespace ProjectSeraphBackend.Domain
{
    /// <summary>
    /// This class represents an alarm generated from a measurement with a specific time and type.
    ///</summary>

    public class Alarm
    {
        public DateTime Time { get; set; }
        public AlarmType AlarmType { get; set; }

        //Måske disse skal bruges til UC5+6 (herunder statistik)
        //for at linke alarm til borger og evt. målingsID

        //public int Id { get; set; }
        //public int CitizenId { get; set; }
        //public int MeasurementId { get; set; }
    }
}
