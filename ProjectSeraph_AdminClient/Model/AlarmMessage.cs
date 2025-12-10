using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    public class AlarmMessage
    {
        public string CitizenID { get; set; }
        public string CitizenName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
