using ProjectSeraph_AdminClient.Model;
using ProjectSeraph_AdminClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ProjectSeraph_AdminClient.ViewModel
{
    /// <summary>
    /// 
    /// </summary>

    public class NurseEditorViewModel : Bindable
    {
        private readonly NurseService _nurseService;
        private readonly IMyNavigationService _navigation;

        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                PropertyIsChanged(nameof(FullName));
            }
        }

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                PropertyIsChanged(nameof(Username));
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyIsChanged(nameof(Password));
            }
        }

        //NURSE PERMISSIONS - EXAMPLES 
        //These can be mapped to a list of permissions in the Nurse model later or to a separate NursePermissions-DTO
        public bool CanAccessCitizenData {get; set; }
        public bool CanSeeMeasurements {get; set; }
        public bool CanSeeAlarms {get; set; }
        public bool CanManageAlarms {get; set; }

        //COMMANDS FOR SAVE AND CANCEL IN NURSE EDITOR
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        //Methods for save and cancel commands in nurse editor
        public NurseEditorViewModel(IMyNavigationService navigationService)
        {
            _nurseService = new NurseService();
            _navigation = navigationService!; 

            SaveCommand = new DelegateCommand<object>(async _ => await OnSaveAsync());
            CancelCommand = new DelegateCommand<object>(_ => OnCancel());
        }

        private async Task OnSaveAsync()
        {
            try
            {
                //KAN SLETTES NÅR ALT FUNGERER
                System.Windows.MessageBox.Show("SaveCommand blev kørt");

                var nurse = new Nurse
                {
                    fullName = FullName,
                    userName = Username,
                    password = Password

                    //SET NURSE PERMISSIONS HERE WHEN THEY ARE ADDED TO THE MODEL
                };
                var created = await _nurseService.CreateAsync(nurse);
                Console.WriteLine("Nurse created: " + nurse);

                if (created == null)
                {
                    //Handle error (not implemented)
                    throw new Exception("Failed to create nurse.");
                }

                //Navigate back to the ManageNursesViewModel after saving
                _navigation.NavigateTo<ManageNursesViewModel>(_navigation);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Crash prevented: {ex.Message}");
            }
        }

        private void OnCancel() 
        {
            //Navigate back to the ManageNursesViewModel without saving
            _navigation.NavigateTo<ManageNursesViewModel>(_navigation);
        }
    }
}
