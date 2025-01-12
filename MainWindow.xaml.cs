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

namespace Whiteboard
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<SchedData> ScheduleData { get; set; }

        public ObservableCollection<DataBar> BottomDataTable { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            ScheduleData = new ObservableCollection<SchedData>();
            BottomDataTable = new ObservableCollection<DataBar>();

            var app = (App)Application.Current;
            app.LoadDataFromFile(ScheduleData);
            app.LoadDataTableFromFile(BottomDataTable);
            this.WindowState = WindowState.Maximized;
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

        private void ProfileEditButton_Click(object sender, RoutedEventArgs e)
        {

            ProfileEditWindow profileWindow = new ProfileEditWindow(ScheduleData);
            profileWindow.Show();
        }
    }
}