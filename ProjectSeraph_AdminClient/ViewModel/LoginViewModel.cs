using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.Services;
using System.Windows;
using System.Windows.Input;

namespace ProjectSeraph_AdminClient.ViewModel
{
    public class LoginViewModel : Bindable
    {
        private readonly IAuthenticationService _authService;
        private readonly IMyNavigationService _navigationService;


        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    PropertyIsChanged();
                    ((DelegateCommand<object>)LoginCommand)?.RaiseCanExecuteChanged();
                }
            }
        } 
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyIsChanged();
                    ((DelegateCommand<object>)LoginCommand)?.RaiseCanExecuteChanged();
                    
                }
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(IMyNavigationService navigationService, IAuthenticationService authService)
        {            
            _navigationService = navigationService;

            _username = string.Empty;
            _password = string.Empty;

            LoginCommand = new DelegateCommand<object>(
                execute: async (_) => await ExecuteLogin(), 
                canExecute: (_) => CanExecuteLogin());
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private async Task ExecuteLogin()
        {
            string userId = null;
            try
            {
                // Mock data for testing
                if(Username == "1111110000" && Password == "admin")
                {
                    userId = Username; 
                }
                else
                {
                    MessageBox.Show("Ugyldig CPR-nummer eller adgangskode.", "Login mislykkedes", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _navigationService.NavigateToMain(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login mislykkedes: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
