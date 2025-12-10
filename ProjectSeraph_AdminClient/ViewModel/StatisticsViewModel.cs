using LiveCharts;
using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts.Wpf;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// Represents a view model for displaying statistical data in a user interface.
    /// </summary>
    /// <remarks>This class is designed to be used in data binding scenarios where statistical data needs to
    /// be presented and updated dynamically. It inherits from the <see cref="Bindable"/> class, allowing it to
    /// participate in property change notifications.</remarks>
    public class StatisticsViewModel : Bindable
    {
        //private readonly HttpClient _http;
        private readonly IMyNavigationService _navigation;
        private readonly MeasurementService _measurementService;

        //Saves all measurements locally - used for filtering
        private MeasurementData[] _allMeasurements = Array.Empty<MeasurementData>();

        //Collection of measurement counts per day for data binding
        public ObservableCollection<MeasurementCountPerDay> MeasurementCounts { get; } = new();

        //LiveCharts-data 
        public SeriesCollection SeriesCollection { get; private set; } = new SeriesCollection();
        public string[] Labels { get; private set; } = Array.Empty<string>();


        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                propertyIsChanged(nameof(SelectedDate));
                UpdateSelectedDateCount();
            }
        }

        private int _selectedDateCount;
        public int SelectedDateCount
        {
            get => _selectedDateCount;
            set
            {
                _selectedDateCount = value;
                propertyIsChanged(nameof(SelectedDateCount));
            }
        }

        //COMMANDS
        public ICommand RefreshCommand { get; }

        //Constructor with command implementations
        public StatisticsViewModel(IMyNavigationService navigationService)
        {
            _navigation = navigationService
                ?? throw new InvalidOperationException("Navigation service is not initialized.");
            
            _measurementService = new MeasurementService();

            RefreshCommand = new DelegateCommand<object>(async _ => await LoadMeasurementsAsync());

            //Initial load
            _ = LoadMeasurementsAsync();
        }

        //Loads all measurements from backend and processes them for statistics + charting
        private async Task LoadMeasurementsAsync() 
        {
            MeasurementCounts.Clear();
            _allMeasurements = Array.Empty<MeasurementData>();

            try 
            {
                var result = (await _measurementService.GetAllAsync()).ToArray();
                _allMeasurements = result;

                //Group by date and count measurements per day
                var grouped = result
                    .GroupBy(m => m.Timestamp.Date)
                    .Select(g => new MeasurementCountPerDay
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Date);
                foreach (var item in grouped)
                    MeasurementCounts.Add(item);

                BuildChartFromMeasurements(); 
                UpdateSelectedDateCount();               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved hentning af målinger: {ex.Message}");
            }
        }

        //Builds the chart data from the measurement counts
        private void BuildChartFromMeasurements() 
        {
            var data = MeasurementCounts.ToList(); 
            Labels = data.Select(d => d.Date.ToString("dd-MM")).ToArray();
            var values = new ChartValues<int>(data.Select(d => d.Count));
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Antal Målinger",
                    Values = values
                }
            };
            propertyIsChanged(nameof(Labels));
            propertyIsChanged(nameof(SeriesCollection));
        }

        //Updates the count of measurements for the selected date
        private void UpdateSelectedDateCount() 
        {
            if( SelectedDate == null || _allMeasurements.Length == 0) 
            {
                SelectedDateCount = 0;
                return;
            }
            var date = SelectedDate.Value.Date;
            SelectedDateCount = _allMeasurements.Count(m => m.Timestamp.Date == date);
        }
    }
}
