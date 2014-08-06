using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AdminClientForUnregUsers
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel _model;
        private DispatcherTimer _dispatcherTimer;
        private int _timerTickCount;

        public MainWindow()
        {
            InitializeComponent();
            _model = new MainWindowViewModel();
            DataContext = _model;
            InitTimer();
            RestartTimer();
        }

        //Creating timer 
        void InitTimer() {
            _dispatcherTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(60)};
            _dispatcherTimer.Tick += (_, a) =>
            {
                if (_timerTickCount-- == 0)
                {
                    _dispatcherTimer.Stop();
                    OnTimerFinish();
                }
                else
                {
                    ShowTickCount();
                }
            };
        }

        //Refresh and Restart when interval = 0
        void OnTimerFinish() {
            RefreshAndRestartTimer();
        }

        //Shows ticks on the screen
        void ShowTickCount() {
            RefreshButton.Content = string.Format("REFRESH({0})", _timerTickCount);
        }

        //Restart timer ( even if refresh button clicked ) 
        void RestartTimer() {
            _dispatcherTimer.Stop();
            _timerTickCount = 10;
            ShowTickCount();
            _dispatcherTimer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshAndRestartTimer();
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e) {
            var button = (Button)sender;
            var result = _model.Submit(Convert.ToInt32(button.Tag));
            if (!result.Success)
            {
                MessageBox.Show(result.ErrorMessage);   
            }
            RefreshAndRestartTimer();
        }

        private void RefreshAndRestartTimer() {            
                _model.Refresh();
                RestartTimer();
        }

    }
}
