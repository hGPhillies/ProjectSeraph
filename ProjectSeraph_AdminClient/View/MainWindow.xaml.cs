using System;
using System.Windows;
using ProjectSeraph_AdminClient.Model;      
using ProjectSeraph_AdminClient.ViewModel; 

namespace ProjectSeraph_AdminClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AlarmService _alertService;

        public MainWindow()
        {
            InitializeComponent();

            // Create navigation service
            var navigationService = new MyNavigationService();

            // Make the navigation service globally accessible
            App.NavigationService = navigationService;

            // Create alarm service
            _alertService = new AlarmService();

            // Set the DataContext to MainViewModel with navigation service
            DataContext = new MainViewModel(navigationService);
        }

        protected override void OnClosed(EventArgs e)
        {
            // Dispose alarm service when window is closed
            _alertService?.Dispose();

            base.OnClosed(e);
        }
    }
}
