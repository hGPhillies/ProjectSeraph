using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.Application.DTO
{
    public class BloodpressureDTO : MeasurementDTO
    {
        public double Systolic { get; set; }
        public double Diastolic { get; set; }

        public BloodpressureDTO()
        {

        }
        public BloodpressureDTO(Measurement m) : base(m) 
        {

            this.Systolic = m.
        }
    }
}
