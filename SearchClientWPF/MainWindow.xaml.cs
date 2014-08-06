namespace SearchClientWPF
{
    using System;
    using System.Collections.Generic;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using SearchClientWPF.SearchServiceReference;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Create event procs.
            btnFindButton.Click += btnFindButton_Click;
            btnAbort.Click += btnAbort_Click;
        }

        //Service Proxy
        SearchServiceClient client;

        //this will make sure that the "ExtentionData" column added as part of Serialization is prevented from showing up on the grid.
        void grdData_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ExtensionData")
            {
                e.Cancel = true;
            }
        }


        //If the request is still being executed, this gives a chance to abort it.
        void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                if (client.State == System.ServiceModel.CommunicationState.Opened)
                {
                    client.Abort();
                }
            }
        }

        //Async method to get the data from web service.
        async void btnFindButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchString = (findTextBox.Text).ToString();

                busyIndicator.IsBusy = true;

                client = new SearchServiceClient();

                //everything after this line is made to wait until the execution is finished.
                //while it waits, the application control is handed over to the calling method.
                //That way, the UI is kept responsive.
                var result = await client.SearchItemsAsync(searchString);

                client.Close();

                grdData.ItemsSource = result;

                busyIndicator.IsBusy = false;

            }
            catch (Exception ex)
            {
                busyIndicator.IsBusy = false;

                //do not show error for deliberately aborted requests.
                if (!ex.Message.Contains("The request was aborted: The request was canceled."))
                {
                    MessageBox.Show("Unexpected error: " + ex.Message,
                        "Async Await Demo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            this.UpdateLayout();
        }
    }
}
