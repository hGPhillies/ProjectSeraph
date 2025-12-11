using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ProjectSeraph_AdminClient.Model
{
    //Subject to change depending on the values we send
    public class MeasurementData
    {
        [JsonPropertyName("citizenId")]
        public string CitizenId { get; set; }

        [JsonPropertyName("citizenName")]
        public string CitizenName { get; set; }

        [JsonPropertyName("measurementType")]
        public string MeasurementType { get; set; } // "BloodPressure", "HeartRate", etc.

        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; } // "mmHg", "bpm", etc.

        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("AlarmType")]
        public string AlarmType { get; set; }
    }
}
