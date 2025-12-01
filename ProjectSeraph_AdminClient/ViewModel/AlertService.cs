using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSeraph_AdminClient.ViewModel
{
    public class AlertService
    {
        public void ShowCriticalAlert(string citizenId, string citizenName)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var alert = new Alert
                {
                    CitizenId = citizenId,
                    CitizenName = citizenName,
                    Timestamp = DateTime.Now,
                };

                var alertWindow = new AlertWindow(alert);
                alertWindow.ShowDialog();
            });
        }

        //Test alert
        public void TestAlert()
        {
            ShowCriticalAlert("citizen_123", "John Hansen");
        }
    }
}
