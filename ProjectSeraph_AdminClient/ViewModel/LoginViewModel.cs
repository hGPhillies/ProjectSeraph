using ProjectSeraph_AdminClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSeraph_AdminClient.ViewModel
{
    public class LoginViewModel : Bindable
    {
        private readonly IMyNavigationService _navigationService;
        public LoginViewModel(IMyNavigationService navigationService)
        {
            _navigationService = navigationService;
            
        }
    }
}
