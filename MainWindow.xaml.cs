using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Whiteboard
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<SchedData> ScheduleData { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ScheduleData = new ObservableCollection<SchedData>();
            DataContext = this;

            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Steven Kotowski",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Brain Dead",
                EmployeeMonday = "8am - 5pm",
                EmployeeTuesday = "8am - 5pm",
                EmployeeWednesday = "8am - 5pm",
                EmployeeThursday = "8am - 5pm",
                EmployeeFriday = "8am - 5pm"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Tony France",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Attentio-what?",
                EmployeeMonday = "5am - 5pm",
                EmployeeTuesday = "5am - 5pm",
                EmployeeWednesday = "5am - 5pm",
                EmployeeThursday = "5am - 5pm",
                EmployeeFriday = "5am - 5pm"
            });

            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Sara Teran",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Cutie Pie",
                EmployeeMonday = "24/7",
                EmployeeTuesday = "24/7",
                EmployeeWednesday = "24/7",
                EmployeeThursday = "24/7",
                EmployeeFriday = "24/7"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Steven Kotowski",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Brain Dead",
                EmployeeMonday = "8am - 5pm",
                EmployeeTuesday = "8am - 5pm",
                EmployeeWednesday = "8am - 5pm",
                EmployeeThursday = "8am - 5pm",
                EmployeeFriday = "8am - 5pm"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Tony France",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Attentio-what?",
                EmployeeMonday = "5am - 5pm",
                EmployeeTuesday = "5am - 5pm",
                EmployeeWednesday = "5am - 5pm",
                EmployeeThursday = "5am - 5pm",
                EmployeeFriday = "5am - 5pm"
            });

            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Sara Teran",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Cutie Pie",
                EmployeeMonday = "24/7",
                EmployeeTuesday = "24/7",
                EmployeeWednesday = "24/7",
                EmployeeThursday = "24/7",
                EmployeeFriday = "24/7"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Steven Kotowski",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Brain Dead",
                EmployeeMonday = "8am - 5pm",
                EmployeeTuesday = "8am - 5pm",
                EmployeeWednesday = "8am - 5pm",
                EmployeeThursday = "8am - 5pm",
                EmployeeFriday = "8am - 5pm"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Tony France",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Attentio-what?",
                EmployeeMonday = "5am - 5pm",
                EmployeeTuesday = "5am - 5pm",
                EmployeeWednesday = "5am - 5pm",
                EmployeeThursday = "5am - 5pm",
                EmployeeFriday = "5am - 5pm"
            });

            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Sara Teran",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Cutie Pie",
                EmployeeMonday = "24/7",
                EmployeeTuesday = "24/7",
                EmployeeWednesday = "24/7",
                EmployeeThursday = "24/7",
                EmployeeFriday = "24/7"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Steven Kotowski",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Brain Dead",
                EmployeeMonday = "8am - 5pm",
                EmployeeTuesday = "8am - 5pm",
                EmployeeWednesday = "8am - 5pm",
                EmployeeThursday = "8am - 5pm",
                EmployeeFriday = "8am - 5pm"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Tony France",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Attentio-what?",
                EmployeeMonday = "5am - 5pm",
                EmployeeTuesday = "5am - 5pm",
                EmployeeWednesday = "5am - 5pm",
                EmployeeThursday = "5am - 5pm",
                EmployeeFriday = "5am - 5pm"
            });

            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Sara Teran",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Cutie Pie",
                EmployeeMonday = "24/7",
                EmployeeTuesday = "24/7",
                EmployeeWednesday = "24/7",
                EmployeeThursday = "24/7",
                EmployeeFriday = "24/7"
            });
            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Tony France",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Attentio-what?",
                EmployeeMonday = "5am - 5pm",
                EmployeeTuesday = "5am - 5pm",
                EmployeeWednesday = "5am - 5pm",
                EmployeeThursday = "5am - 5pm",
                EmployeeFriday = "5am - 5pm"
            });

            ScheduleData.Add(new SchedData()
            {
                EmployeeName = "Sara Teran",
                EmployeePhoto = "Images/EmpPhoto.png",
                EmployeeCurrentStatus = "Cutie Pie",
                EmployeeMonday = "24/7",
                EmployeeTuesday = "24/7",
                EmployeeWednesday = "24/7",
                EmployeeThursday = "24/7",
                EmployeeFriday = "24/7"
            });
        }

        // Core functions such as top buttons and drag/minimize/close functionality
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1920;
                    this.Height = 1080;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
                }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Logic regarding property updates
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






    }
}