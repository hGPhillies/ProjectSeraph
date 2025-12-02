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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;


namespace ProjectSeraph_AdminClient.View
{
    /// <summary>
    /// Interaction logic for CitizenView.xaml
    /// </summary>
    public partial class CitizenView : UserControl
    {
        public CitizenView()
        {
            InitializeComponent();
        }

        private void CreateCitizen_Click(object sender, RoutedEventArgs e)
        {
            // Show the modal popup
            ModalOverlay.Visibility = Visibility.Visible;

            // Clear previous inputs
            ClearForm();

            // Focus on first field
            LastNameTextBox.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Hide the modal
            ModalOverlay.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CitizenIDTextBox.Text) ||
                    string.IsNullOrWhiteSpace(StreetNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(HouseNumberTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PostalCodeTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CityTextBox.Text))
                {
                    MessageBox.Show("Please fill in all required fields (marked with *)",
                                  "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Parse floor number (optional field)
                int floorNumber = 0;
                if (!string.IsNullOrWhiteSpace(FloorNumberTextBox.Text))
                {
                    if (!int.TryParse(FloorNumberTextBox.Text, out floorNumber))
                    {
                        MessageBox.Show("Floor number must be a valid integer",
                                      "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Create citizen object
                var citizenData = new
                {
                    lastName = LastNameTextBox.Text.Trim(),
                    firstName = FirstNameTextBox.Text.Trim(),
                    citizenID = CitizenIDTextBox.Text.Trim(),
                    home = new
                    {
                        streetName = StreetNameTextBox.Text.Trim(),
                        houseNumber = HouseNumberTextBox.Text.Trim(),
                        postalCode = PostalCodeTextBox.Text.Trim(),
                        city = CityTextBox.Text.Trim(),
                        floorNumber = floorNumber,
                        door = DoorTextBox.Text.Trim()
                    }
                };

                // Convert to JSON (for API call or saving)
                string json = JsonConvert.SerializeObject(citizenData, Formatting.Indented);

                // TODO: Add your save logic here (API call, database, etc.)
                MessageBox.Show($"Citizen created successfully!\n\nJSON:\n{json}",
                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Hide modal after successful save
                ModalOverlay.Visibility = Visibility.Collapsed;

                // Clear form for next use
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving citizen: {ex.Message}",
                              "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearForm()
        {
            LastNameTextBox.Text = "";
            FirstNameTextBox.Text = "";
            CitizenIDTextBox.Text = "";
            StreetNameTextBox.Text = "";
            HouseNumberTextBox.Text = "";
            PostalCodeTextBox.Text = "";
            CityTextBox.Text = "";
            FloorNumberTextBox.Text = "";
            DoorTextBox.Text = "";
        }

        // Optional: Handle Enter key to move between fields
        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var currentTextBox = sender as TextBox;
                var parent = currentTextBox?.Parent as Panel;

                if (parent != null)
                {
                    int currentIndex = parent.Children.IndexOf(currentTextBox);
                    if (currentIndex < parent.Children.Count - 1)
                    {
                        var nextControl = parent.Children[currentIndex + 1];
                        if (nextControl is TextBox nextTextBox)
                        {
                            nextTextBox.Focus();
                        }
                    }
                }
            }
        }
    }
}