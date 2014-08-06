namespace AdminClient
{
    using AdminClient.UserManagementServiceReference;
    using SchoolLibrary.BusinessModels.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManagementServiceClient client;

        public MainWindow()
        {
            InitializeComponent();
            this.client = new UserManagementServiceClient();
            this.UpdateGrid();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            for(var vis=sender as Visual; vis!=null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow) vis;
                    int userId = (row.Item as UserProfileBusinessModel).UserId;
                    this.client.DeleteUser(userId);
                    this.userProfileBusinessModelDataGrid.Items.Remove(row.Item);
                }
        }

        private void ResetPassword(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    int userId = (row.Item as UserProfileBusinessModel).UserId;
                    this.client.ResetPassword(userId);
                }
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.client.Close();
        }

        private void UpdateGrid()
        {
            var u = this.client.GetAllUsers();

            this.userProfileBusinessModelDataGrid.Items.Clear();

            foreach (var us in u)
            {
                this.userProfileBusinessModelDataGrid.Items.Add(us);
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.UpdateGrid();
        }
    }
}