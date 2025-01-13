using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Linq;
using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Whiteboard
{
    // Define the Classes for the Collections
    public class SchedData : INotifyPropertyChanged
    {
        // Property Changed Handler
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Define the Properties
        private string? _employeeName;
        private string? _employeeTitle;
        private string? _employeePhoto;
        private string? _employeeCurrentStatus;
        private string? _employeeMondayStart;
        private string? _employeeTuesdayStart;
        private string? _employeeWednesdayStart;
        private string? _employeeThursdayStart;
        private string? _employeeFridayStart;
        private string? _employeeMondayEnd;
        private string? _employeeTuesdayEnd;
        private string? _employeeWednesdayEnd;
        private string? _employeeThursdayEnd;
        private string? _employeeFridayEnd;
        private string? _employeeAccountName;



        // Define the Getters and Setters
        public string? EmployeeName
        {
            get => _employeeName;
            set { _employeeName = value; OnPropertyChanged(nameof(EmployeeName)); }
        }
        public string? EmployeeTitle
        {
            get => _employeeTitle;
            set { _employeeTitle = value; OnPropertyChanged(nameof(EmployeeTitle)); }
        }
        public string? EmployeePhoto
        {
            get => _employeePhoto;
            set { _employeePhoto = value; OnPropertyChanged(nameof(EmployeePhoto)); }
        }
        public string? EmployeeCurrentStatus
        {
            get => _employeeCurrentStatus;
            set { _employeeCurrentStatus = value; OnPropertyChanged(nameof(EmployeeCurrentStatus)); }
        }
        public string? EmployeeMondayStart
        {
            get => _employeeMondayStart;
            set { _employeeMondayStart = value; OnPropertyChanged(nameof(EmployeeMondayStart)); }
        }
        public string? EmployeeTuesdayStart
        {
            get => _employeeTuesdayStart;
            set { _employeeTuesdayStart = value; OnPropertyChanged(nameof(EmployeeTuesdayStart)); }
        }
        public string? EmployeeWednesdayStart
        {
            get => _employeeWednesdayStart;
            set { _employeeWednesdayStart = value; OnPropertyChanged(nameof(EmployeeWednesdayStart)); }
        }
        public string? EmployeeThursdayStart
        {
            get => _employeeThursdayStart;
            set { _employeeThursdayStart = value; OnPropertyChanged(nameof(EmployeeThursdayStart)); }
        }
        public string? EmployeeFridayStart
        {
            get => _employeeFridayStart;
            set { _employeeFridayStart = value; OnPropertyChanged(nameof(EmployeeFridayStart)); }
        }
        public string? EmployeeMondayEnd
        {
            get => _employeeMondayEnd;
            set { _employeeMondayEnd = value; OnPropertyChanged(nameof(EmployeeMondayEnd)); }
        }
        public string? EmployeeTuesdayEnd
        {
            get => _employeeTuesdayEnd;
            set { _employeeTuesdayEnd = value; OnPropertyChanged(nameof(EmployeeTuesdayEnd)); }
        }
        public string? EmployeeWednesdayEnd
        {
            get => _employeeWednesdayEnd;
            set { _employeeWednesdayEnd = value; OnPropertyChanged(nameof(EmployeeWednesdayEnd)); }
        }
        public string? EmployeeThursdayEnd
        {
            get => _employeeThursdayEnd;
            set { _employeeThursdayEnd = value; OnPropertyChanged(nameof(EmployeeThursdayEnd)); }
        }
        public string? EmployeeFridayEnd
        {
            get => _employeeFridayEnd;
            set { _employeeFridayEnd = value; OnPropertyChanged(nameof(EmployeeFridayEnd)); }
        }
        public string? EmployeeAccountName
        {
            get => _employeeAccountName;
            set { _employeeAccountName = value; OnPropertyChanged(nameof(EmployeeAccountName)); }
        }
    }
    public class DataBar : INotifyPropertyChanged
    {
        // Property Changed Handler
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Define the Properties
        public string? EventDate1 { get; set; }
        public string? EventDescription1 { get; set; }
        public string? EventDate2 { get; set; }
        public string? EventDescription2 { get; set; }
        public string? EventDate3 { get; set; }
        public string? EventDescription3 { get; set; }
        public string? EventDate4 { get; set; }
        public string? EventDescription4 { get; set; }
        public string? TriviaQuestion { get; set; }
        public string? TriviaAnswer { get; set; }
        public string? Contact1 { get; set; }
        public string? Contact2 { get; set; }
        public string? Contact3 { get; set; }


        // Define the Getters and Setters
        // TBD
    };



    // Application Constructor
    public partial class App : Application
    {
        public void SaveDataToFile(ObservableCollection<SchedData> scheduleData)
        {
            try
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var dataDirectory = Path.Combine(baseDirectory, "Data");

                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                var saveFilePath = Path.Combine(dataDirectory, "EmployeeData.csv");

                var configCsv = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };

                using (var writer = new StreamWriter(saveFilePath))
                using (var csv = new CsvWriter(writer, configCsv))
                {
                    csv.WriteRecords(scheduleData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void LoadDataFromFile(ObservableCollection<SchedData> scheduleData)
        {
            try
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var dataDirectory = Path.Combine(baseDirectory, "Data");
                var openFilePath = Path.Combine(dataDirectory, "EmployeeData.csv");

                if (!File.Exists(openFilePath))
                {
                    Console.WriteLine("The file does not exist.");
                    return;
                }

                var configCsv = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };

                using (var reader = new StreamReader(openFilePath))
                using (var csv = new CsvReader(reader, configCsv))
                {
                    var records = csv.GetRecords<SchedData>().ToList();
                    scheduleData.Clear();

                    foreach (var record in records)
                    {
                        scheduleData.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void LoadDataTableFromFile(ObservableCollection<DataBar> BottomTableCollection)
        {
            try
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var dataDirectory = Path.Combine(baseDirectory, "Data");
                var openFilePath = Path.Combine(dataDirectory, "DataTable.csv");
                if (!File.Exists(openFilePath))
                {
                    Console.WriteLine("The file does not exist.");
                    return;
                }
                var configCsv = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };
                using (var reader = new StreamReader(openFilePath))
                using (var csv = new CsvReader(reader, configCsv))
                {
                    var datarecords = csv.GetRecords<DataBar>().ToList();
                    BottomTableCollection.Clear();
                    foreach (var databarrecord in datarecords)
                    {
                        BottomTableCollection.Add(databarrecord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public App()
        {
            // Application initialization logic here (if needed)
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start the SignalR server in a background task
            Task.Run(() => DataSendReceiveProgram.MainTransfer(new string[] { }));

            // Initialize and show the main window (no need for this.Run() since it's automatic)
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }




    public class DataHub : Hub
    {
        private static List<SchedData> _schedDataCollection = new List<SchedData>();

        // Method to update employee data (called by the client)
        public async Task UpdateEmployeeData(string username, string propertyName, string newValue)
        {
            // Check if the username matches any employee in the collection
            var employee = _schedDataCollection.FirstOrDefault(e => e.EmployeeName.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (employee != null)
            {
                // Match found, update the employee's property
                switch (propertyName)
                {
                    case "EmployeeCurrentStatus":
                        employee.EmployeeCurrentStatus = newValue;
                        break;

                    default:
                        // Handle unrecognized property names if necessary
                        break;
                }

                // Notify all clients about the update
                await Clients.All.SendAsync("EmployeeDataUpdated", username, propertyName, newValue);
            }
            else
            {
                // If no matching employee found, do nothing
                // You could log or notify that no update was made (optional)
                Console.WriteLine($"No matching employee found for username: {username}");
            }
        }
    }

    public class DataSendReceiveProgram
    {
        public static void MainTransfer(string[] args)
        {
            try
            {
                Console.WriteLine("Initializing SignalR server...");

                // Create WebApplicationBuilder
                var builder = WebApplication.CreateBuilder(args);

                // Add SignalR services
                builder.Services.AddSignalR();

                // Explicitly configure Kestrel to listen on port 9852
                builder.WebHost.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.ListenLocalhost(9852); // Ensure that Kestrel listens on port 9852
                });

                // Create WebApplication
                var app = builder.Build();

                // Set up routing and SignalR hub mapping
                app.UseRouting();
                app.MapHub<DataHub>("/Whiteboard");

                // Debugging message to confirm that the server is running
                Console.WriteLine("SignalR server should be running now.");

                // Run the application in the background without blocking the UI thread
                Task.Run(() => app.Run()); // Running the SignalR server on a separate task
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during server setup: {ex.Message}");
            }
        }

    }

}
