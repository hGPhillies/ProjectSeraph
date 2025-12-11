using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    /// <summary>
    /// Represents the count of measurements taken on a specific day.
    /// Used for statistics views to display daily measurement counts.
    /// </summary>
    public class MeasurementCountPerDay
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
