using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Whiteboard
{
    public partial class App : Application
    {

    }

    public class SchedData : INotifyPropertyChanged
    {
        private string _employeeName;
        private string _employeePhoto;
        private string _employeeCurrentStatus;
        private string _employeeMonday;
        private string _employeeTuesday;
        private string _employeeWednesday;
        private string _employeeThursday;
        private string _employeeFriday;

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

        public string EmployeeMonday
        {
            get => _employeeMonday;
            set { _employeeMonday = value; OnPropertyChanged(nameof(EmployeeMonday)); }
        }

        public string EmployeeTuesday
        {
            get => _employeeTuesday;
            set { _employeeTuesday = value; OnPropertyChanged(nameof(EmployeeTuesday)); }
        }
        public string EmployeeWednesday
        {
            get => _employeeWednesday;
            set { _employeeWednesday = value; OnPropertyChanged(nameof(EmployeeWednesday)); }
        }
        public string EmployeeThursday
        {
            get => _employeeThursday;
            set { _employeeThursday = value; OnPropertyChanged(nameof(EmployeeThursday)); }
        }
        public string EmployeeFriday
        {
            get => _employeeFriday;
            set { _employeeFriday = value; OnPropertyChanged(nameof(EmployeeFriday)); }
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
    }

}
