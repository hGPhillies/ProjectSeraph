using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectSeraph_AdminClient.View
{
    /// <summary>
    /// Interaction logic for Alertwindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public Alert Alert { get; }
        public AlertWindow(Alert alert)
        {
            InitializeComponent();
            Alert = alert;
            DataContext = alert;
        }

        private void Acknowledge_Click(object sender, RoutedEventArgs e)
        {
            //TODO : Navigate to citizen details page in main application
            // _navigationService.NavigateToCitizenDetails(Alert.CitizenId);
            Close();
        }
    }
}
