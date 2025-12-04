using ProjectSeraph_AdminClient.Model;
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
        private readonly INavigationService _navigation;

        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                propertyIsChanged(nameof(FullName));
            }
        }

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                propertyIsChanged(nameof(Username));
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                propertyIsChanged(nameof(Password));
            }
        }

        //NURSE PERMISSIONS - EXAMPLES 
        //These can be mapped to a list of permissions in the Nurse model later or to a separate NursePermissions-DTO
        public bool CanAccessCitizenData {get; set; }
        public bool CanSeeMeasurements {get; set; }
        public bool CanSeeAlarms {get; set; }
        public bool CanManageAlarms {get; set; }


        //COMMANDS FOR SAVE AND CANCEL IN NURSE EDITOR
        public ICommand SaveCommand;
        public ICommand CancelCommand;

        //Methods for save and cancel commands in nurse editor
        public NurseEditorViewModel(NurseService nurseService, INavigationService navigation)
        {
            _nurseService = nurseService;
            _navigation = navigation;
            SaveCommand = new DelegateCommand<object>(_ => OnSave());
            CancelCommand = new DelegateCommand<object>(_ => OnCancel());
        }

        private async void OnSave()
        {
            var nurse = new Nurse
            {
                fullName = FullName,
                userName = Username,
                password = Password
                //SET NURSE RIGHTS HERE WHEN THEY ARE ADDED TO THE MODEL
            };
            await _nurseService.CreateAsync(nurse);

            //Navigate back to the ManageNursesViewModel after saving
            _navigation.NavigateTo<ManageNursesViewModel>();
        }

        private async void OnCancel() 
        {
            //Navigate back to the ManageNursesViewModel without saving
            _navigation.NavigateTo<ManageNursesViewModel>();
        }
    }
}
