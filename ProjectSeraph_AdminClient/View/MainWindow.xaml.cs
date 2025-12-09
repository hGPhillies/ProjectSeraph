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
    }
}