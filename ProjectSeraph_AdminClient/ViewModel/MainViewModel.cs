using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.Services;
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
        private readonly AuthenticationService _authService;
        private readonly WebSocketClientService _webSocketClient;
        private Bindable _currentViewModel;
        private bool _isNavigationVisible;

        public AlarmService AlarmService => _alarmService;
        public ICommand NavigationCommand { get; }
        public ICommand TestAlarmCommand { get; }

        public MainViewModel()
        {
            _webSocketClient = new WebSocketClientService("wss://localhost:5001/ws/alarms");
            _alarmService = new AlarmService(_webSocketClient);
            _navigationService = new MyNavigationService();
            _authService = new AuthenticationService();
            NavigationCommand = new DelegateCommand<string>(NavigateTo);
            TestAlarmCommand = new DelegateCommand<object>(_ => ExecuteTestAlarm());
            _navigationService.LoginSuccessful += OnLoginSuccessful;

            _navigationService.CurrentViewModelChanged += (viewModel) =>
            {
                CurrentViewModel = viewModel;
            };
                
            var loginViewModel = new LoginViewModel(_navigationService, _authService);

            CurrentViewModel = loginViewModel;

            ((DelegateCommand<object>)loginViewModel.LoginCommand)?.RaiseCanExecuteChanged();
        }

        private void OnLoginSuccessful(string username)
        {
            LoggedInUserID = username;
                
            IsNavigationVisible = true;
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
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    PropertyIsChanged(nameof(CurrentViewModel));
                    
                }
            }
        }

        private string _loggedInUserID;
        public string LoggedInUserID
        {
            get => _loggedInUserID;
            private set
            {
                if (_loggedInUserID != value)
                {
                    _loggedInUserID = value;
                    PropertyIsChanged(nameof(LoggedInUserID));
                }
            }
        }

        public bool IsNavigationVisible
        {
            get => _isNavigationVisible;
            private set
            {
                if (_isNavigationVisible != value)
                {
                    _isNavigationVisible = value;
                    PropertyIsChanged(nameof(IsNavigationVisible));
                }
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
                    _navigationService.NavigateTo<StatisticsViewModel>(_navigationService);
                    break;

                case "Manage Nurses":
                    _navigationService.NavigateTo<ManageNursesViewModel>(_navigationService);
                    break;

                case "Manage Citizen":
                    _navigationService.NavigateTo<ManageCitizenViewModel>(_navigationService);
                    break;              


            }
        }
    }
}
