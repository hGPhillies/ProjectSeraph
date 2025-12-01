using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    public class Alert : Bindable
    {
        public string CitizenId { get; set; }
        public string CitizenName { get; set; }
        public DateTime Timestamp { get; set; }
        public string FormattedTime => Timestamp.ToString("HH:mm:ss");
        public string FormattedDate => Timestamp.ToString("dd/MM/yyyy");

        public string displayText => $"{CitizenName} - {Timestamp:HH:mm}";
    }
}
