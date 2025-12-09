using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    public class Alarm : Bindable
    {
        // Websocket transfered properties        
        public string CitizenId { get; set; }
        public DateTime Timestamp { get; set; }
        public string CitizenName { get; set; }


        // Additional properties for REST endpoint 
        public string MeasurementType { get; set; }
        public double MeasurementValue { get; set; }



        public string DisplayInfo => $"{CitizenName} - {Timestamp:HH:mm:ss}";
        public string FormattedTime => Timestamp.ToString("HH:mm:ss");        
    }
}
