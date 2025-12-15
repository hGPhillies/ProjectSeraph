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
   /// Represents a view model for displaying and managing statistics related to measurements.
   /// </summary>
   /// <remarks>This view model provides functionality to load measurement data, process it for statistical
   /// analysis, and update UI elements such as charts and measurement counts. It supports data binding for measurement
   /// counts per day and chart data, and includes a command for refreshing the data.</remarks>
    public class StatisticsViewModel : Bindable
    {
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
        //Selected date for filtering measurement count
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                PropertyIsChanged(nameof(SelectedDate));
                UpdateSelectedDateCount();
            }
        }

        private int _selectedDateCount;
        //Count of measurements for the selected date
        public int SelectedDateCount
        {
            get => _selectedDateCount;
            set
            {
                _selectedDateCount = value;
                PropertyIsChanged(nameof(SelectedDateCount));
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
                    .GroupBy(m => m.Timestamp.Date) //LINQ query to group by date
                    .Select(g => new MeasurementCountPerDay 
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(x => x.Date); //LINQ query to group and count measurements
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
            PropertyIsChanged(nameof(Labels));
            PropertyIsChanged(nameof(SeriesCollection));
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
