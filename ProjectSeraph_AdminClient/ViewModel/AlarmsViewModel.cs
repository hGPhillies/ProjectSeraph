using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.ViewModel
{

    /// <summary>
    /// Represents the view model for managing alarms in the application.
    /// </summary>
    /// <remarks>This class provides properties and methods to interact with and manipulate alarm data,
    /// supporting data binding scenarios in the user interface.</remarks>
    class AlarmsViewModel : Bindable
    {
        public string Title => "Alarm View Model";
        public string TestContent => "This is the Alarm View Model content.";
    }
}
