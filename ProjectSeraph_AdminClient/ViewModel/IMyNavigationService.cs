using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// Provides navigation capabilities within the application, allowing transitions between different view models.
    /// </summary>
    /// <remarks>This service facilitates navigation by transitioning to specified view models, optionally
    /// passing parameters. It also provides an event to notify when the current view model changes.</remarks>
    public interface IMyNavigationService
    {
        void NavigateTo<T>() where T : Bindable;
        void NavigateTo<T>(object parameter) where T : Bindable;
        event Action<Bindable> CurrentViewModelChanged;
    }
}
