using ProjectSeraph_AdminClient.Model;

using System.Windows.Input;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// Represents the main view model responsible for managing navigation and the current view model in the
    /// application.
    /// </summary>
    /// <remarks>This view model utilizes an <see cref="INavigationService"/> to handle navigation between
    /// different view models. It subscribes to changes in the current view model and updates the <see
    /// cref="CurrentViewModel"/> property accordingly.</remarks>
    public class MainViewModel : Bindable
    {
        private readonly AlarmService _alarmService;
        private readonly MyNavigationService _navigationService;
        private Bindable _currentViewModel;

        public AlarmService AlarmService => _alarmService;
        public ICommand NavigationCommand { get; }
        public ICommand TestAlarmCommand { get; }

        public MainViewModel()
        {
            var webSocketClient = new WebSocketClientService("wss://localhost:5001/ws/alarms");
            _alarmService = new AlarmService(webSocketClient);
            _navigationService = new MyNavigationService();
            NavigationCommand = new DelegateCommand<string>(NavigateTo);
            TestAlarmCommand = new DelegateCommand<object>(_ => ExecuteTestAlarm());


            _navigationService.CurrentViewModelChanged += (viewModel) =>
            {
                CurrentViewModel = viewModel;
            };
                
            _navigationService.NavigateTo<CitizenViewModel>(_navigationService);
        }

        private void ExecuteTestAlarm()
        {
            _alarmService.TestAlarm();
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
        
        private void NavigateTo(string viewType)
        {
            switch(viewType)
            {
                case "Citizen":
                    _navigationService.NavigateTo<CitizenViewModel>(_navigationService);
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
                    _navigationService.NavigateTo<ManageNursesViewModel>(_navigationService);
                    break;

                case "Manage Citizen":
                    _navigationService.NavigateTo<ManageCitizenViewModel>();
                    break;

            }
        }
    }
}
