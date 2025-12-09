using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Model
{
    public class Citizen
    {
        // Basic person data
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string CitizenID { get; set; } = string.Empty;

        // Address object – this is the one you new'er op i ViewModel:
        // Home = new Home { StreetName = ..., HouseNumber = ... }
        public Home Home { get; set; } = new Home();

        public int Age { get; set; }



        // Citizen permissions
        public bool CanMeasureBloodPressure { get; set; }
        public bool CanMeasureBloodSugar { get; set; }
    }

    public class Home
    {
        // These match the fields we bind to in the ViewModel:
        // StreetName, HouseNumber, PostalCode, City, FloorNumber, Door
        public string StreetName { get; set; } = string.Empty;
        public string HouseNumber { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int FloorNumber { get; set; }
        public string Door { get; set; } = string.Empty;
    }
}
