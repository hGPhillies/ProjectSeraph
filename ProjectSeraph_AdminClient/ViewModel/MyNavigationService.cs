using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ProjectSeraph_AdminClient.Viewmodel
{
    /// <summary>
    /// Provides navigation functionality for switching between view models in an application.
    /// </summary>
    /// <remarks>The <see cref="NavigationService"/> class allows navigation to different view models by
    /// creating instances of them and raising the <see cref="CurrentViewModelChanged"/> event. It supports navigation
    /// with or without parameters.</remarks>
    class MyNavigationService : INavigationService
    {
        public event Action<Bindable> CurrentViewModelChanged;

        public void NavigateTo<T>() where T : Bindable
        {
            var viewModel = Activator.CreateInstance<T>();
            CurrentViewModelChanged?.Invoke(viewModel);
        }

        public void NavigateTo<T>(object parameter) where T : Bindable
        {
            var viewModel = Activator.CreateInstance(typeof(T), parameter) as Bindable;
            CurrentViewModelChanged?.Invoke(viewModel);
        }
    }
}
