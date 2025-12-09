using ProjectSeraph_AdminClient.Model;

using ProjectSeraph_AdminClient.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            var navigationService = new MyNavigationService();

            //Makes the navigation service globally accessible
            App.NavigationService = navigationService;

            
            _alertService = new AlarmService();

            //Set the DataContext to MainViewModel with navigation service
            DataContext = new MainViewModel(navigationService);
        }        
    }
}