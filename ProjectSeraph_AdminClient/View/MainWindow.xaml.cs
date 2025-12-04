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
        private readonly AlertService _alertService;
        public MainWindow()
        {
            InitializeComponent();

            var navigationService = new MyNavigationService();
            _alertService = new AlertService();
            DataContext = new MainViewModel(navigationService);
        }

        private void TestAlertButton_Click(object sender, RoutedEventArgs e)
        {
            _alertService.TestAlert();
        }
    }
}