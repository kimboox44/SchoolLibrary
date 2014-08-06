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
using LibrarianClient.Service;
namespace LibrarianClient
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReaderManagmentClient client;

        private ObservableCollection<ReaderViewModel> readers; 

        public ObservableCollection<ReaderViewModel> Readers {
            get
            {
                return this.readers;
            }

            set
            {
                this.readers = value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.client = new ReaderManagmentClient();
            this.initCollection();
        }

        private void initCollection()
        {
            var readersFromService = this.client.GetAllReaders();
            this.readers = new ObservableCollection<ReaderViewModel>();
            foreach (var reader in readersFromService)
            {
                var readerViewModel = (ReaderViewModel)reader;
                readerViewModel.PropertyChanged += this.OnPropertyChanged;
                this.readers.Add(readerViewModel);
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.client.UpdateReader((ReaderBusinessModel)((ReaderViewModel)sender));
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            client.Close();
        }
    }
}
