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

namespace Whiteboard
{
    // Define the Classes for the Collections
    public class SchedData : INotifyPropertyChanged
    {
        // Property Changed Handler
        public event PropertyChangedEventHandler PropertyChanged;
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
        public string EmployeeName
        {
            get => _employeeName;
            set { _employeeName = value; OnPropertyChanged(nameof(EmployeeName)); }
        }
        public string EmployeeTitle
        {
            get => _employeeTitle;
            set { _employeeTitle = value; OnPropertyChanged(nameof(EmployeeTitle)); }
        }
        public string EmployeePhoto
        {
            get => _employeePhoto;
            set { _employeePhoto = value; OnPropertyChanged(nameof(EmployeePhoto)); }
        }
        public string EmployeeCurrentStatus
        {
            get => _employeeCurrentStatus;
            set { _employeeCurrentStatus = value; OnPropertyChanged(nameof(EmployeeCurrentStatus)); }
        }
        public string EmployeeMondayStart
        {
            get => _employeeMondayStart;
            set { _employeeMondayStart = value; OnPropertyChanged(nameof(EmployeeMondayStart)); }
        }
        public string EmployeeTuesdayStart
        {
            get => _employeeTuesdayStart;
            set { _employeeTuesdayStart = value; OnPropertyChanged(nameof(EmployeeTuesdayStart)); }
        }
        public string EmployeeWednesdayStart
        {
            get => _employeeWednesdayStart;
            set { _employeeWednesdayStart = value; OnPropertyChanged(nameof(EmployeeWednesdayStart)); }
        }
        public string EmployeeThursdayStart
        {
            get => _employeeThursdayStart;
            set { _employeeThursdayStart = value; OnPropertyChanged(nameof(EmployeeThursdayStart)); }
        }
        public string EmployeeFridayStart
        {
            get => _employeeFridayStart;
            set { _employeeFridayStart = value; OnPropertyChanged(nameof(EmployeeFridayStart)); }
        }
        public string EmployeeMondayEnd
        {
            get => _employeeMondayEnd;
            set { _employeeMondayEnd = value; OnPropertyChanged(nameof(EmployeeMondayEnd)); }
        }
        public string EmployeeTuesdayEnd
        {
            get => _employeeTuesdayEnd;
            set { _employeeTuesdayEnd = value; OnPropertyChanged(nameof(EmployeeTuesdayEnd)); }
        }
        public string EmployeeWednesdayEnd
        {
            get => _employeeWednesdayEnd;
            set { _employeeWednesdayEnd = value; OnPropertyChanged(nameof(EmployeeWednesdayEnd)); }
        }
        public string EmployeeThursdayEnd
        {
            get => _employeeThursdayEnd;
            set { _employeeThursdayEnd = value; OnPropertyChanged(nameof(EmployeeThursdayEnd)); }
        }
        public string EmployeeFridayEnd
        {
            get => _employeeFridayEnd;
            set { _employeeFridayEnd = value; OnPropertyChanged(nameof(EmployeeFridayEnd)); }
        }
        public string EmployeeAccountName
        {
            get => _employeeAccountName;
            set { _employeeAccountName = value; OnPropertyChanged(nameof(EmployeeAccountName)); }
        }
    }
    public class DataBar : INotifyPropertyChanged
    {
        // Property Changed Handler
        public event PropertyChangedEventHandler PropertyChanged;
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
    }
}
