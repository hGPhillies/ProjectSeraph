using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.Viewmodel
{
    /// <summary>
    /// Represents a view model for a citizen, providing data binding capabilities.
    /// </summary>
    
    class CitizenViewModel : Bindable
    {
        public string Title => "Citizen View Model";
        public string TestContent => "This is the Citizen View Model content.";
    }
}
