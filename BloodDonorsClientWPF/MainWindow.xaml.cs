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
using BloodDonorsClientWPF.DonorPages;
using BloodDonorsClientWPF.PersonnelPages;
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

            var personnelClient = clientFactory.GetPersonnelClient();
            var donorClient = clientFactory.GetDonorClient();

            personnelClient.OnLogout += WhenPersonnelLoggedOff;
            donorClient.OnLogout += WhenDonorLoggedOff;

            personnelClient.OnLogin += WhenPersonnelLoggedIn;
            donorClient.OnLogin += WhenDonorLoggedIn;
        }

        private void WhenDonorLoggedIn(object sender, EventArgs e)
        {
            DonorAuthorizedButtonsStackPanel.Visibility = Visibility.Visible;
        }

        private void WhenPersonnelLoggedIn(object sender, EventArgs e)
        {
            PersonnelAuthorizedButtonsStackPanel.Visibility = Visibility.Visible;
        }

        private void WhenDonorLoggedOff(object sender, EventArgs e)
        {
            DonorAuthorizedButtonsStackPanel.Visibility = Visibility.Collapsed;
            AfterLogOff();
        }

        private void WhenPersonnelLoggedOff(object sender, EventArgs e)
        {
            PersonnelAuthorizedButtonsStackPanel.Visibility = Visibility.Collapsed;
            AfterLogOff();
        }

        private void AfterLogOff()
        {
            MainFrame.Content = new MainPage(clientFactory);
            DialogAutomaticLogOff.IsOpen = true;
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

        private void ShowMainPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MainPage(clientFactory);
        }

        private void ShowDonorLoginPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DonorLoginPage(clientFactory);
        }

        private void ShowDonorAccountPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DonorAccountPage(clientFactory);
        }


        private void ShowPersonnelLoginPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PersonnelLoginPage(clientFactory);
        }

        private void ShowPersonnelAccountPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PersonnelAccountPage(clientFactory);
        }

        private void ShowRegisterNewDonorPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new RegisterNewDonorPage(clientFactory);
        }

        private void ShowAddNewDonationPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PersonnelAddNewDonationPage(clientFactory);
        }

        private void ShowPersonnelGetDonorAccountPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PersonnelGetDonorsInfoPage(clientFactory);
        }

        private void ShowPersonnelDonationsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PersonnelDonationsPage(clientFactory);
        }
    }
}
