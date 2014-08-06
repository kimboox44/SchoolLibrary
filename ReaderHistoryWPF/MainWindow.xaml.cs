using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
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
using ReaderHistoryWPF.ServiceReference1;

namespace ReaderHistoryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReaderHistoryServiceClient client = new ReaderHistoryServiceClient();
        string login;
        string password;
        ReaderBusinessModel reader;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.historyBusinessModelForGridListView.Items.Clear();
            try
            {
                int days = Convert.ToInt32(daysTextBox.Text);
                var readerHistoryList = client.GetStudentsBooksToReturn(reader.ReaderId, days);

                if (readerHistoryList == null)
                {
                    MessageBox.Show("During these days, you are not required to bring a book");
                    return;

                }
                titleLabel.Content = "Next " + days + " days you are required to bring a books to the School Library";

                foreach (var readerHistory in readerHistoryList)
                {
                    this.historyBusinessModelForGridListView.Items.Add(readerHistory);

                }
            }
            catch
            {
                MessageBox.Show("Please Enter number of days in the fild or login");
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            titleLabel.Content = "Please enter number of days:";

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.historyBusinessModelForGridListView.Items.Clear();
            titleLabel.Content = "";
            login = loginTextBox.Text;
            password = passwordTextBox.Password;
            reader = client.Login(login, password);
            if (reader != null)
            {
                readerProfileLabel.Content = "Welcome:  " + reader.FirstName + "  " + reader.LastName;
            }
            else
            {
                MessageBox.Show("Your login or password is wrong");
            }
        }

        private void historyBusinessModelForGridListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
