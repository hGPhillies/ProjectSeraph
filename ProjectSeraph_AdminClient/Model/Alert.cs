using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    public class Alert : Bindable
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public string CitizenName { get; set; }

        public string MeasurementType { get; set; }        

        public DateTime Timestamp { get; set; }


        public string FormattedTime => Timestamp.ToString("HH:mm:ss");        
        public string displayInfo => $"{CitizenName} - {MeasurementType}";
    }
}
