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
using BloodDonorsClientLibrary.Services;
using MaterialDesignThemes.Wpf;

namespace BloodDonorsClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClientFactory clientFactory;
        private readonly MiscellaneousClient miscellaneousClient;

        public MainWindow()
        {
            InitializeComponent();
            clientFactory = new ClientFactory();
            MainFrame.Content = new MainPage(clientFactory);

            miscellaneousClient = clientFactory.GetMiscellaneousClient();
        }

        private void ShowServersAvailability(object sender, RoutedEventArgs e)
        {
            DialogHostIsServerOnline.IsOpen = true;
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            DialogHostAbout.IsOpen = true;
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogHostExit.IsOpen = true;
        }
        

        private void HeaderOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void ExitDialog_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            if(Equals(eventargs.Parameter,true))
                Close();
        }

        private async void IsServerOnlineDialog_OnDialogOpening(object sender, DialogOpenedEventArgs eventargs)
        {
            ServerStatusProgressBar.Visibility = Visibility.Visible;
            ServerStatus.Text = "";

            var isServerOnline = await miscellaneousClient.IsServerOnline();

            ServerStatusProgressBar.Visibility = Visibility.Collapsed;
            if (isServerOnline)
            {
                ServerStatus.Text = "ONLINE";
                ServerStatus.Foreground = Brushes.Green;
            }
            else
            {
                ServerStatus.Text = "OFFLINE";
                ServerStatus.Foreground = Brushes.DarkRed;
            }
        }
    }
}
