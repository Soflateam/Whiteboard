﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;



namespace Whiteboard
{
    public partial class ProfileEditWindow : Window, INotifyPropertyChanged
    {
        // Establish Variables
        private SchedData? _selectedEmployee;
        public SchedData? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }


        // Profile Window Constructor
        public ProfileEditWindow(ObservableCollection<SchedData> TempScheduleData, MainWindow mainWindow)
        {
            InitializeComponent();

            this.DataContext = this;

            EmployeeDataGrid.ItemsSource = TempScheduleData;

        }



        // Core functions such as top buttons and drag/minimize/close and Property Change logic
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Picture Related Functions
        private void UploadPictureButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee == null)
            {
                MessageBox.Show("No employee selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(SelectedEmployee.EmployeeName))
            {
                MessageBox.Show("Please enter a Name first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create an OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Select a Picture"
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                string sourceFile = openFileDialog.FileName;
                string destinationDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmployeePhotos");

                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                // Access the ImageBrush by its name and clear the ImageSource
                var imageBrush = (ImageBrush)this.FindName("EmployeeImageListView");
                if (imageBrush != null)
                {
                    imageBrush.ImageSource = null;  // Release old image
                }

                // Search for old photos in the directory containing the employee's name
                var oldFiles = Directory.GetFiles(destinationDirectory)
                                        .Where(file => file.Contains(SelectedEmployee.EmployeeName, StringComparison.OrdinalIgnoreCase))
                                        .ToList();

                // Delete old files if they exist
                foreach (var oldFile in oldFiles)
                {
                    try
                    {
                        // Check if the file is in use (locked) before deleting
                        if (IsFileLocked(oldFile))
                        {
                            continue;
                        }

                        File.Delete(oldFile);  // Try to delete the old image
                    }
                    catch
                    {
                        // Handle exceptions if necessary
                    }
                }

                // Sanitize the employee's name to create a valid file name
                string sanitizedEmployeeName = string.Join("_", SelectedEmployee.EmployeeName.Split(System.IO.Path.GetInvalidFileNameChars()));
                string fileExtension = System.IO.Path.GetExtension(sourceFile);
                string destinationPath = System.IO.Path.Combine(destinationDirectory, $"{sanitizedEmployeeName}{fileExtension}");

                // Ensure no overwrites by appending a number if the file already exists
                int counter = 1;
                while (File.Exists(destinationPath))
                {
                    destinationPath = System.IO.Path.Combine(destinationDirectory, $"{sanitizedEmployeeName}_{counter}{fileExtension}");
                    counter++;
                }

                try
                {
                    // Load the image
                    using (Bitmap sourceBitmap = new Bitmap(sourceFile))
                    {
                        RotateImageIfNeeded(sourceBitmap);

                        // Set the maximum width or height for the scaled image
                        int maxWidth = 500;
                        int maxHeight = 500;

                        // Calculate the aspect ratio
                        float aspectRatio = (float)sourceBitmap.Width / sourceBitmap.Height;

                        int newWidth = maxWidth;
                        int newHeight = (int)(maxWidth / aspectRatio); // Scale height based on width

                        // If the calculated height exceeds the max height, adjust the width
                        if (newHeight > maxHeight)
                        {
                            newHeight = maxHeight;
                            newWidth = (int)(maxHeight * aspectRatio); // Scale width based on height
                        }

                        // Create a new resized bitmap while maintaining the aspect ratio
                        using (Bitmap resizedBitmap = new Bitmap(sourceBitmap, new System.Drawing.Size(newWidth, newHeight)))
                        {
                            // Save the resized image to the destination path
                            resizedBitmap.Save(destinationPath, ImageFormat.Jpeg);
                        }
                    }

                    // Update the SelectedEmployee's ImagePath property with the new file path (string)
                    SelectedEmployee.EmployeePhoto = destinationPath;

                    // Refresh the Image control
                    if (SelectedEmployee.EmployeePhoto != null)
                    {
                        // Update the Source of the Image control to show the new picture
                        EmployeePhoto.Source = new BitmapImage(new Uri(destinationPath));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading photo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RotateImageIfNeeded(Bitmap image)
        {
            const int orientationId = 0x0112; // EXIF tag for orientation
            if (image.PropertyIdList.Contains(orientationId))
            {
                int orientationValue = image.GetPropertyItem(orientationId).Value[0];

                switch (orientationValue)
                {
                    case 3:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 6:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 8:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
            }
        }

        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
                return false;
        }



        // Custom Click Functions
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee == null)
            {
                MessageBox.Show("No employee selected.", "Cannot Save", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var saveResponse = MessageBox.Show("Do you want to save changes to the employee?", "Save", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (saveResponse == MessageBoxResult.Yes)
            {

                // Ensure that a valid employee is selected
                if (SelectedEmployee != null)
                {

                    SelectedEmployee.EmployeeName = NameTextBox.Text;
                    SelectedEmployee.EmployeeTitle = TitleTextBox.Text;
                    SelectedEmployee.EmployeeAccountName = AccountNameTextBox.Text;
                    SelectedEmployee.EmployeeMondayStart = MondayStartTextBox.Text;
                    SelectedEmployee.EmployeeMondayEnd = MondayEndTextBox.Text;
                    SelectedEmployee.EmployeeTuesdayStart = TuesdayStartTextBox.Text;
                    SelectedEmployee.EmployeeTuesdayEnd = TuesdayEndTextBox.Text;
                    SelectedEmployee.EmployeeWednesdayStart = WednesdayStartTextBox.Text;
                    SelectedEmployee.EmployeeWednesdayEnd = WednesdayEndTextBox.Text;
                    SelectedEmployee.EmployeeThursdayStart = ThursdayStartTextBox.Text;
                    SelectedEmployee.EmployeeThursdayEnd = ThursdayEndTextBox.Text;
                    SelectedEmployee.EmployeeFridayStart = FridayStartTextBox.Text;
                    SelectedEmployee.EmployeeFridayEnd = FridayEndTextBox.Text;


                    OnPropertyChanged(nameof(SelectedEmployee));

                    EmployeeDataGrid.Items.Refresh();

                    ((App)Application.Current).SaveDataToFile(EmployeeDataGrid.ItemsSource as ObservableCollection<SchedData>);

                    MessageBox.Show("Changes Saved!\n\n You must restart the application for changes to take effect.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();

                }
                else
                {

                }
            }
        }

        public void DiscardButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearEmployeeEditForm()
        {
            // Clear all textboxes
            NameTextBox.Clear();
            MondayStartTextBox.Clear();
            MondayEndTextBox.Clear();
            TuesdayStartTextBox.Clear();
            TuesdayEndTextBox.Clear();
            WednesdayStartTextBox.Clear();
            WednesdayEndTextBox.Clear();
            ThursdayStartTextBox.Clear();
            ThursdayEndTextBox.Clear();
            FridayStartTextBox.Clear();
            FridayEndTextBox.Clear();

            // Reset the image to the placeholder or the original image
            EmployeePhoto.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/EmployeePlaceholderPhoto.png"));

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeList = EmployeeDataGrid.ItemsSource as ObservableCollection<SchedData>;

            if (employeeList.Count >= 16)
            {
                MessageBox.Show("You cannot add more than 16 employees.", "Limit Reached", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Prepare form for new data
            ClearEmployeeEditForm();

                // Create a new employee (SchedData) with data from the TextBoxes
                var newEmployee = new SchedData
                {
                    EmployeeName = NameTextBox.Text,
                    EmployeePhoto = "pack://application:,,,/Assets/Images/EmployeePlaceholderPhoto.png",
                    EmployeeMondayStart = MondayStartTextBox.Text,
                    EmployeeMondayEnd = MondayEndTextBox.Text,
                    EmployeeTuesdayStart = TuesdayStartTextBox.Text,
                    EmployeeTuesdayEnd = TuesdayEndTextBox.Text,
                    EmployeeWednesdayStart = WednesdayStartTextBox.Text,
                    EmployeeWednesdayEnd = WednesdayEndTextBox.Text,
                    EmployeeThursdayStart = ThursdayStartTextBox.Text,
                    EmployeeThursdayEnd = ThursdayEndTextBox.Text,
                    EmployeeFridayStart = FridayStartTextBox.Text,
                    EmployeeFridayEnd = FridayEndTextBox.Text
                };

                // Add the new employee to the ObservableCollection
                ((ObservableCollection<SchedData>)EmployeeDataGrid.ItemsSource).Add(newEmployee);

                // Save the changes to file
                ((App)Application.Current).SaveDataToFile(EmployeeDataGrid.ItemsSource as ObservableCollection<SchedData>);

                // Optionally, refresh the DataGrid if needed
                EmployeeDataGrid.Items.Refresh();

                // Auto-select the newly added employee
                EmployeeDataGrid.SelectedItem = newEmployee;
        }
                
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure that an employee is selected
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirm removal
            var confirmResponse = MessageBox.Show($"Are you sure you want to remove {SelectedEmployee.EmployeeName}?\n\nThis is Permanent and cannot be recovered.",
                                                   "Remove Employee", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmResponse == MessageBoxResult.Yes)
            {
                // Remove the selected employee from the ObservableCollection
                ((ObservableCollection<SchedData>)EmployeeDataGrid.ItemsSource).Remove(SelectedEmployee);

                // Optionally, refresh the DataGrid if needed
                EmployeeDataGrid.Items.Refresh();

                // Save the changes to file
                ((App)Application.Current).SaveDataToFile(EmployeeDataGrid.ItemsSource as ObservableCollection<SchedData>);

                // Clear the form
                ClearEmployeeEditForm();
            }
        }

        private void UpArrowButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure that the ItemsSource of the DataGrid is an ObservableCollection<SchedData>
            var selectedItem = (SchedData)EmployeeDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var employeeList = EmployeeDataGrid.ItemsSource as ObservableCollection<SchedData>;
                if (employeeList == null) return;

                int currentIndex = employeeList.IndexOf(selectedItem);

                // Check if the item is not already the first in the list
                if (currentIndex > 0)
                {
                    // Swap the selected item with the one in front of it
                    employeeList.Move(currentIndex, currentIndex - 1);
                }
            }
        }

        private void DownArrowButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure that the ItemsSource of the DataGrid is an ObservableCollection<SchedData>
            var selectedItem = (SchedData)EmployeeDataGrid.SelectedItem;

            if (selectedItem != null)
            {
                var employeeList = EmployeeDataGrid.ItemsSource as ObservableCollection<SchedData>;
                if (employeeList == null) return; 

                int currentIndex = employeeList.IndexOf(selectedItem);

                // Check if the item is not already the last in the list
                if (currentIndex < employeeList.Count - 1)
                {
                    // Swap the selected item with the one below it
                    employeeList.Move(currentIndex, currentIndex + 1);
                }
            }
        }

    }
}

   