using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Whiteboard
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Establish Collections
        private DispatcherTimer _timer;

        public ObservableCollection<SchedData> ScheduleData { get; set; }

        public ObservableCollection<DataBar> BottomDataTable { get; set; }

        public ObservableCollection<SchedData> TempScheduleData { get; set; }



        // Main Window Constructor
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            ScheduleData = new ObservableCollection<SchedData>();
            TempScheduleData = new ObservableCollection<SchedData>();
            BottomDataTable = new ObservableCollection<DataBar>();

            var app = (App)Application.Current;
            app.LoadDataFromFile(ScheduleData);
            app.LoadDataFromFile(TempScheduleData);
            app.LoadDataTableFromFile(BottomDataTable);
            this.WindowState = WindowState.Maximized;

            // Create and set up the DispatcherTimer
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the TextBlock with the current time
            ClockText.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        // Core functions such as top buttons and drag/minimize/close and Property Changed handler
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Custom Click Functions
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ProfileEditButton_Click(object sender, RoutedEventArgs e)
        {
            var profileEditWindow = new ProfileEditWindow(TempScheduleData, this);
            profileEditWindow.ShowDialog();
        }
    }
}