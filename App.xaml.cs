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

namespace Whiteboard
{
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
    }

    // Define collections
    public class SchedData : INotifyPropertyChanged
    {
        private string? _employeeName;
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

        public string EmployeeName
        {
            get => _employeeName;
            set { _employeeName = value; OnPropertyChanged(nameof(EmployeeName)); }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EventInfo
    {
        public string EventDate { get; set; }
        public string EventDescription { get; set; }
    };

}
