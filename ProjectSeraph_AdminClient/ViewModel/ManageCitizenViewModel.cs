using Newtonsoft.Json;
using ProjectSeraph_AdminClient.Model;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace ProjectSeraph_AdminClient.ViewModel
{
    public class ManageCitizenViewModel : Bindable
    {
        //public string Title => "Manage Citizen Model";
        //public string TestContent => "This is the Manage Citizen Model content.";

        #region Properties 
        //Formular fields
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; propertyIsChanged(nameof(LastName)); }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; propertyIsChanged(nameof(FirstName)); }
        }

        private string _streetName;
        public string StreetName
        {
            get => _streetName;
            set { _streetName = value; propertyIsChanged(nameof(StreetName)); }
        }
        private string _houseNumber;
        public string HouseNumber
        {
            get => _houseNumber;
            set { _houseNumber = value; propertyIsChanged(nameof(HouseNumber)); }
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set { _postalCode = value; propertyIsChanged(nameof(PostalCode)); }
        }

        private string _city;
        public string City
        {
            get => _city;
            set { _city = value; propertyIsChanged(nameof(City)); }
        }

        private string _floorNumber;
        public string FloorNumber
        {
            get => _floorNumber;
            set { _floorNumber = value; propertyIsChanged(nameof(FloorNumber)); }
        }

        private string _door;
        public string Door
        {
            get => _door;
            set { _door = value; propertyIsChanged(nameof(Door)); }
        }
        #endregion

        #region CitizenPermissions
        //Citizen permissions
        private bool _canMeasureBloodPressure;
        public bool CanMeasureBloodPressure
        {
            get => _canMeasureBloodPressure;
            set { _canMeasureBloodPressure = value; propertyIsChanged(nameof(CanMeasureBloodPressure)); }
        }

        private bool _canMeasureBloodSugar;
        public bool CanMeasureBloodSugar
        {
            get => _canMeasureBloodSugar;
            set { _canMeasureBloodSugar = value; propertyIsChanged(nameof(CanMeasureBloodSugar)); }
        }
        #endregion

        private bool _isCreateCitizenVisible;
        public bool IsCreateCitizenVisible
        {
            get => _isCreateCitizenVisible;
            set { _isCreateCitizenVisible = value; propertyIsChanged(nameof(IsCreateCitizenVisible)); }
        }

        //COMMANDS
        public ICommand OpenCreateCitizenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        //Constructor with command implementations
        public ManageCitizenViewModel()
        {
            //Commands
            OpenCreateCitizenCommand = new DelegateCommand<object>(_ => OnOpenCreateCitizen());
            SaveCommand = new DelegateCommand<object>(async _ => await OnSaveCitizenAsync()); 
            CancelCommand = new DelegateCommand<object>(_ => OnCancel());
        }

        private void OnOpenCreateCitizen()
        {
            ClearForm();
            IsCreateCitizenVisible = true;
        }

        private void OnCancel()
        {
            IsCreateCitizenVisible = false;
        }

        private async Task OnSaveCitizenAsync()
        {
            try
            {
                //Validate required fields
                if (string.IsNullOrWhiteSpace(LastName) ||
                    string.IsNullOrWhiteSpace(FirstName) ||
                    string.IsNullOrWhiteSpace(StreetName) ||
                    string.IsNullOrWhiteSpace(HouseNumber) ||
                    string.IsNullOrWhiteSpace(PostalCode) ||
                    string.IsNullOrWhiteSpace(City))
                {
                    MessageBox.Show("Please fill in all required fields (marked with *)",
                                  "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //Parse floor number (optional field)
                int floorNumber = 0;
                if (!string.IsNullOrWhiteSpace(FloorNumber))
                {
                    if (!int.TryParse(FloorNumber, out floorNumber))
                    {
                        MessageBox.Show("Indtast etagenummer mellem 0-100",
                                      "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                //Create Citizen object
                var citizenData = new
                {
                    lastName = LastName.Trim(),
                    firstName = FirstName.Trim(),
                    citizenID = String.Empty,
                    home = new
                    {
                        streetName = StreetName.Trim(),
                        houseNumber = HouseNumber.Trim(),
                        postalCode = PostalCode.Trim(),
                        city = City.Trim(),
                        floorNumber = floorNumber,
                        door = (Door ?? string.Empty).Trim()
                    },
                    CanMeasureBloodPressure = CanMeasureBloodPressure,
                    CanMeasureBloodSugar = CanMeasureBloodSugar
                };

                //Convert to JSON (for API call or saving)
                string json = JsonConvert.SerializeObject(citizenData, Formatting.Indented);

                //TODO: Add the save logic here API call to backend
                MessageBox.Show($"Citizen created successfully!\n\nJSON:\n{json}",
                                "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                //Close the form
                IsCreateCitizenVisible = false;

                //Clear form for next use
                ClearForm();
            }
            catch
            {
                MessageBox.Show($"Error saving citizen: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await Task.CompletedTask;
        }

        private void ClearForm()
        {
            LastName = "";
            FirstName = "";
            StreetName = "";
            HouseNumber = "";
            PostalCode = "";
            City = "";
            FloorNumber = "";
            Door = "";
            CanMeasureBloodPressure = false;
            CanMeasureBloodSugar = false;
        }
    }
}
