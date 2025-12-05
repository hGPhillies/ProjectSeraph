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
    public class AlarmService 
    {
        public void ShowCriticalAlarm(string citizenId, string citizenName)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var alarm = new Alarm
                {
                    CitizenId = citizenId,
                    CitizenName = citizenName,
                    Timestamp = DateTime.Now,
                };

                var alarmPopUpWindow = new AlarmPopUpWindow(alarm);
                alarmPopUpWindow.ShowDialog();
            });
        }

        
    }
}
