using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// Represents a view model for a citizen, providing data binding capabilities.
    /// </summary>
    
    public class CitizenViewModel : Bindable, INotifyPropertyChanged
    {
        private readonly HttpClient _http;

        private readonly IMyNavigationService _navigation;

        public ObservableCollection<Citizen> Citizens { get; } = new();
        public ObservableCollection<MeasurementData> Measurements { get; } = new();
        public ObservableCollection<Citizen> FilteredCitizens { get; } = new();

        private Citizen? _selectedCitizen;

        public Citizen? SelectedCitizen
        {
            get => _selectedCitizen;
            set
            {
                _selectedCitizen = value;
                propertyIsChanged(nameof(SelectedCitizen));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                // Keep the search box visible in the UI
                // Filtering will be implemented later.
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCitizensCommand { get; }
        public ICommand RefreshMeasurementsCommand { get; }

        public CitizenViewModel()
        {
            _navigation = App.NavigationService
                ?? throw new InvalidOperationException("Navigation service is not initialized.");
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001")
            };

            RefreshCitizensCommand = new DelegateCommand<object>(async _ => await LoadCitizensAsync());
            RefreshMeasurementsCommand = new DelegateCommand<object>(async _ => await LoadMeasurementAsync());

            _ = LoadCitizensAsync();
        }

        public async Task LoadCitizensAsync()
        {
            await LoadMeasurementAsync();

            var result = await _http.GetFromJsonAsync<IEnumerable<Citizen>>("/citizen/getAll");
            Citizens.Clear();
            FilteredCitizens.Clear();
            if (result != null)
            {
                var latestByCitizen = BuildLatestMeasurementMap();
                foreach (var citizen in result)
                {
                    if (latestByCitizen.TryGetValue(citizen.citizenID, out var latest))
                    {
                        citizen.LatestMeasurement = $"{latest.MeasurementType}: {latest.Value}{latest.Unit} ({latest.Timestamp:g})";
                    }
                    else
                    {
                        citizen.LatestMeasurement = "No measurements";
                    }

                    Citizens.Add(citizen);
                    FilteredCitizens.Add(citizen);
                }
            }
        }

        public async Task LoadMeasurementAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<MeasurementData>>("/measurement/getAll");
            Measurements.Clear();
            if (result != null)
            {
                foreach (var measurement in result)
                {
                    Measurements.Add(measurement);
                }
            }
        }

        private Dictionary<string, MeasurementData> BuildLatestMeasurementMap()
        {
            return Measurements
                .GroupBy(m => m.CitizenId)
                .Select(g => g.OrderByDescending(m => m.Timestamp).First())
                .ToDictionary(m => m.CitizenId, m => m);
        }

        // Keep ApplyFilter implementation for future use 
        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCitizens.Clear();
                foreach (var c in Citizens)
                    FilteredCitizens.Add(c);
                return;
            }

            var lower = SearchText.ToLower();

            var filtered = Citizens.Where(c =>
                (c.fullName != null && c.fullName.ToLower().Contains(lower)) ||
                c.citizenID.ToString().ToLower().Contains(lower)
            );

            FilteredCitizens.Clear();
            foreach (var citizen in filtered)
                FilteredCitizens.Add(citizen);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
