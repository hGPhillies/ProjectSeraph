using ProjectSeraphBackend.Domain;

namespace ProjectSeraphBackend.Application.Services
{
    /// <summary>
    /// This service class provides methods to check and classify medical measurements
    /// into alarm types (Green, Yellow, Red) based on predefined thresholds.
    /// </summary>
    public class CheckMeasurement
    {
        #region Measurement Thresholds
        // Threshold values for medical measurements.
        // These ranges are general reference values (not personalized for a specific citizen).

        // Normal ranges (Green zone) — measurement is considered healthy within these limits.
        //When outside these ranges, the measurement is considered abnormal (Yellow zone).
        private const double NormalSysLow = 90;
        private const double NormalSysHigh = 140;
        private const double NormalDiaLow = 60;
        private const double NormalDiaHigh = 90;

        private const double BloodSugarLow = 4.0;
        private const double BloodSugarHigh = 7.0;

        private const double PulseLow = 50;
        private const double PulseHigh = 100;

        // Critical limits (Red zone) — values far outside normal ranges and considered dangerous.
        private const double BloodpressureSysRedLow = 80; 
        private const double BloodpressureSysRedHigh = 180;
        private const double BloodpressureDiaRedLow = 60;
        private const double BloodpressureDiaRedHigh = 120;

        private const double PulseRedLow = 40;
        private const double PulseRedHigh = 130;

        private const double BloodsugarRedLow = 3.0;
        private const double BloodsugarRedHigh = 13.0;
        #endregion

        /// <summary>
        /// This method compares a measurement to normal values
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns> True if normal(green), false if abnormal (yellow or red) </returns>
        public bool CompareToNormal(Measurement measurement)
        {
            return CheckCriticality(measurement) == AlarmType.Green;
        }

        /// <summary>
        /// Classifies the criticality of a given measurement.
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns>An AlarmType indication whether the measurement is Green, Yellow or Red</returns>
        public AlarmType CheckCriticality(Measurement measurement)
        {
            return measurement switch
            {
                Bloodpressure bp => ClassifyBloodpressure(bp),
                Bloodsugar bs => ClassifyBloodsugar(bs),
                Pulse p => ClassifyPulse(p),

                // Default case
                _ => AlarmType.Yellow
            };
        }

        #region Classification Methods
        private AlarmType ClassifyBloodpressure(Bloodpressure bp)
        {
            //Red alarm
            if (bp.Systolic < BloodpressureSysRedLow || bp.Systolic > BloodpressureSysRedHigh ||
                bp.Diastolic < BloodpressureDiaRedLow || bp.Diastolic > BloodpressureDiaRedHigh)
            {
                return AlarmType.Red;
            }
            //Yellow alarm
            else if (bp.Systolic < NormalSysLow || bp.Systolic > NormalSysHigh ||
                     bp.Diastolic < NormalDiaLow || bp.Diastolic > NormalDiaHigh)
            {
                return AlarmType.Yellow;
            }
            //Green alarm
            else
            {
                return AlarmType.Green;
            }
        }
           
        private AlarmType ClassifyPulse(Pulse p)
        {
            //Red alarm
            if (p.BeatsPerMinute < PulseRedLow || p.BeatsPerMinute > PulseRedHigh)
            {
                return AlarmType.Red;
            }
            //Yellow alarm
            else if (p.BeatsPerMinute < PulseLow || p.BeatsPerMinute > PulseHigh)
            {
                return AlarmType.Yellow;
            }
            //Green alarm
            else
            {
                return AlarmType.Green;
            }
        }

        private AlarmType ClassifyBloodsugar(Bloodsugar bs) 
        {
            //Red alarm
            if (bs.millimolePerLiter < BloodsugarRedLow || bs.millimolePerLiter > BloodsugarRedHigh)
            {
                return AlarmType.Red;
            }
            //Yellow alarm
            else if (bs.millimolePerLiter < BloodSugarLow || bs.millimolePerLiter > BloodSugarHigh)
            {
                return AlarmType.Yellow;
            }
            //Green alarm
            else
            {
                return AlarmType.Green;
            }
        }
        #endregion
    }
}