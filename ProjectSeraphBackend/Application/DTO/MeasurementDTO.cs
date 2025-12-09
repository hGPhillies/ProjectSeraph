using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.Application.DTO
{
    public abstract class MeasurementDTO
    {
        public string? MeasurementID { get; set; }
        public string CitizenID { get; set; }
        // Timestamp of the measurement
        public DateTime Time { get; set; }

        public MeasurementDTO() { }

        public MeasurementDTO(Measurement m)
        {
            MeasurementID = m.MeasurementID;
            CitizenID = m.CitizenID;
            Time = m.Time;
        }
    }
}
