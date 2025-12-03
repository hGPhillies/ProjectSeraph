using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectSeraph_AdminClient.Viewmodel
{
    /// <summary>
    /// Represents the main view model responsible for managing navigation and the current view model in the
    /// application.
    /// </summary>
    /// <remarks>This view model utilizes an <see cref="INavigationService"/> to handle navigation between
    /// different view models. It subscribes to changes in the current view model and updates the <see
    /// cref="CurrentViewModel"/> property accordingly.</remarks>
    class MainViewModel : Bindable
    {
        private Bindable _currentViewModel;
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.CurrentViewModelChanged += (viewModel) =>
            {
                CurrentViewModel = viewModel;
            };

            // Simulate an incoming critical alert after 3 seconds (Will be removed)
            Task.Delay(3000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var alertService = new AlertService();
                    alertService.ShowCriticalAlert("citizen_456", "Anna Jensen");
                });
            });

            _navigationService.NavigateTo<CitizenViewModel>();
        }

        public Bindable CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                propertyIsChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand NavigationCommand => new DelegateCommand<string>(NavigateTo);
        
        private void NavigateTo(string viewType)
        {
            switch(viewType)
            {
                case "Citizen":
                    _navigationService.NavigateTo<CitizenViewModel>();
                    break;

                case "Measurements":
                    _navigationService.NavigateTo<MeasurementsViewModel>();
                    break;

                case "Alarms":
                    _navigationService.NavigateTo<AlarmsViewModel>();
                    break;

                case "Contact":
                    _navigationService.NavigateTo<ContactViewModel>();
                    break;

                case "Statistics":
                    _navigationService.NavigateTo<StatisticsViewModel>();
                    break;

                case "Manage Nurses":
                    _navigationService.NavigateTo<ManageNursesViewModel>();
                    break;

            }
        }
    }
}
