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
using System.Globalization;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace Whiteboard
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Establish Collections and Variables
        private DispatcherTimer? _timer;

        private bool _isNetworkAvailable;
        public bool IsNetworkAvailable
        {
            get => _isNetworkAvailable;
            set
            {
                if (_isNetworkAvailable != value)
                {
                    _isNetworkAvailable = value;
                    OnPropertyChanged(nameof(IsNetworkAvailable));
                }
            }
        }

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

            // Network status check
            IsNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
            NetworkChange.NetworkAvailabilityChanged += (s, e) =>
            {
                Dispatcher.Invoke(() => IsNetworkAvailable = e.IsAvailable);
            };
        }




        // Property Changed logic
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        // Clock logic
        private void Timer_Tick(object? sender, EventArgs e)
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

    public class NetworkStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isConnected)
            {
                var result = isConnected ? Brushes.LightGreen : Brushes.Red;
                Debug.WriteLine($"Convert: {isConnected} => {result}");
                return result;
            }
            Debug.WriteLine("Convert: UnsetValue returned");
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("NetworkStatusToColorConverter does not support ConvertBack.");
        }
    }

}