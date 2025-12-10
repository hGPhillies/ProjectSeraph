using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    //Subject to change depending on the values we send
    public class MeasurementData
    {
        [JsonProperty("citizenId")]
        public string CitizenID { get; set; }

        [JsonProperty("citizenName")]
        public string CitizenName { get; set; }

        [JsonProperty("measurementType")]
        public string MeasurementType { get; set; } // "BloodPressure", "HeartRate", etc.

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; } // "mmHg", "bpm", etc.

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("AlarmType")]
        public string AlarmType { get; set; }
    }
}
