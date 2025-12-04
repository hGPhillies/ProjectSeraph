using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;


namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// View model for managing nurse in adminclient application.
    /// Provides a list of nurses and commands for interacting with them (refresh, create, edit).
    /// Inherits from Bindable to support data binding in the UI.
    /// Allows interaction with a backend API to fetch and manipulate nurse information.
    /// </summary>

    public class ManageNursesViewModel : Bindable
    {
        // HTTP client for communicating with the backend API
        private readonly HttpClient _http;
        // Navigation service for navigating between views
        private readonly INavigationService _navigation;

        // Collection of nurses to be displayed in the UI
        public ObservableCollection<Nurse> Nurses { get; } = new();

        private Nurse? _selectedNurse;

        // The currently selected nurse in the UI
        public Nurse? SelectedNurse
        {
            get => _selectedNurse;
            set
            {
                _selectedNurse = value;
                propertyIsChanged(nameof(SelectedNurse));
            }
        }

        // Commands for refreshing the list, creating a new nurse, and editing an existing nurse
        public ICommand RefreshCommand { get; }
        public ICommand CreateNurseCommand { get; }
        public ICommand EditNurseCommand { get; }

        // Constructor initializes commands and loads initial data
        public ManageNursesViewModel()
        {

            //Use global navigation service from App class
            _navigation = App.NavigationService 
                ?? throw new InvalidOperationException("Navigation service is not initialized.");
            
            _http = new HttpClient
            {
                // Make sure backend uses this URL and port
                BaseAddress = new Uri("https://localhost:5001")
            };

            //COMMAND INITIALIZATION

            //FOR TESTING PURPOSES ONLY - load data synchronously - remove when backend is ready
            //RefreshCommand = new DelegateCommand<object> (_ => LoadNurses());

            // ASYNC version – uncomment when backend is ready                                                                             
            RefreshCommand = new DelegateCommand<object>(async _ => await LoadNursesAsync());

            CreateNurseCommand = new DelegateCommand<object>(_ => OnCreateNurse());
            EditNurseCommand = new DelegateCommand<Nurse?>(n => OnEditNurse(n));

            //FOR TESTING PURPOSES ONLY - load data synchronously - remove when backend is ready
            //LoadNurses();

            // ASYNC version – uncomment when backend is ready
            // load initial data
            _ = LoadNursesAsync();

        }

        //FOR TESTING PURPOSES ONLY - load data synchronously - remove when backend is ready
        //private void LoadNurses()
        //{
        //    Nurses.Clear();

        //    // midlertidig testdata – bare for at se at UI virker
        //    Nurses.Add(new Nurse { nurseID = "1", fullName = "Anna Test", userName = "anna" });
        //    Nurses.Add(new Nurse { nurseID = "2", fullName = "Bo Test", userName = "bo" });
        //}
        // ASYNC version – uncomment when backend is ready
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
            _navigation.NavigateTo<NurseEditorViewModel>();
        }

        private void OnEditNurse(Nurse? nurse)
        {
            if (nurse is null) return;
            // TODO: Navigate to the NurseEditorViewModel with the selected nurse 
            //OBS. felterne skal være udfyldt med den valgte nurses data
        }
    }
}
