using ProjectSeraph_AdminClient.ViewModel;
using System.Windows;


namespace ProjectSeraph_AdminClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();                     

        }

        protected override void OnClosed(EventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.AlarmService?.Dispose();   
            }
            base.OnClosed(e);
        }        
            //Makes the navigation service globally accessible
            App.NavigationService = navigationService;

            
            _alertService = new AlarmService();

            //Set the DataContext to MainViewModel with navigation service
            DataContext = new MainViewModel(navigationService);
        }        
    }
}