using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// Viewmodel for managing nurses in the admin client.
    /// Provides a list of nurses and commands for refreshing, creating and editing them.
    /// </summary>
    public class ManageNursesViewModel : Bindable
    {
        // HTTP client for communicating with the backend API
        private readonly HttpClient _http;

        // Navigation service for switching between views
        private readonly INavigationService _navigation;

        // Collection of nurses displayed in the UI
        public ObservableCollection<Nurse> Nurses { get; } = new();

        private Nurse? _selectedNurse;
        public Nurse? SelectedNurse
        {
            get => _selectedNurse;
            set
            {
                _selectedNurse = value;
                propertyIsChanged(nameof(SelectedNurse));
            }
        }

        // Commands
        public ICommand RefreshCommand { get; }
        public ICommand CreateNurseCommand { get; }
        public ICommand EditNurseCommand { get; }

        public ManageNursesViewModel()
        {
            // Global navigation service from App
            _navigation = App.NavigationService
                ?? throw new InvalidOperationException("Navigation service is not initialized.");

            _http = new HttpClient
            {
                //Should match backend
                BaseAddress = new Uri("https://localhost:5001")
            };

            // Commands
            RefreshCommand = new DelegateCommand<object>(async _ => await LoadNursesAsync());
            CreateNurseCommand = new DelegateCommand<object>(_ => OnCreateNurse());
            EditNurseCommand = new DelegateCommand<Nurse?>(n => OnEditNurse(n));

            //Initial load
            _ = LoadNursesAsync();
        }

        private async Task LoadNursesAsync()
        {
            Nurses.Clear();

            var result = await _http.GetFromJsonAsync<Nurse[]>("/nurse/getAll")
                         ?? Array.Empty<Nurse>();

            foreach (var nurse in result)
            {
                Nurses.Add(nurse);
            }
        }

        private void OnCreateNurse()
        {
            //Navigate to the NurseEditorViewModel to create a new nurse
            _navigation.NavigateTo<NurseEditorViewModel>();
        }

        private void OnEditNurse(Nurse? nurse)
        {
            if (nurse is null)
                return;

            // TODO: naviger til editoren med den valgte nurse som parameter
            // _navigation.NavigateTo<NurseEditorViewModel>(nurse);
        }
    }
}
