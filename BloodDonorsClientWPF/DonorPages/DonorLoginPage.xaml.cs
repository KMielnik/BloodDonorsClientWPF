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
using BloodDonorsClientLibrary.Exceptions;
using BloodDonorsClientLibrary.Services;
using MaterialDesignThemes.Wpf;

namespace BloodDonorsClientWPF.DonorPages
{
    /// <summary>
    /// Interaction logic for DonorLoginPage.xaml
    /// </summary>
    public partial class DonorLoginPage : Page
    {
        private readonly DonorClient donorClient;

        public DonorLoginPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            donorClient = clientFactory.GetDonorClient();

            Loaded += LoginPage_Loaded;
            LoginSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1200));
        }

        private async void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            await SetLoggedDonorsPeselInHeader();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var pesel = PeselTextBox.Text;
            if (pesel == DonorPeselTextBlock.Text)
            {
                LoginSnackbar.MessageQueue.Enqueue("You are already logged in");
            }

            var password = PasswordTextBox.Password;

            try
            {
                await donorClient.LoginAsync(pesel, password);
            }
            catch (InvalidLoginCredentialsException)
            {
                LoginSnackbar.MessageQueue.Enqueue("Invalid credentials");
                return;
            }

            var donorName = await donorClient.GetNameAsync();
            LoginSnackbar.MessageQueue.Enqueue("You have been successfully logged in");
            LoginSnackbar.MessageQueue.Enqueue($"Welcome {donorName}");
            await SetLoggedDonorsPeselInHeader();
        }

        private async Task SetLoggedDonorsPeselInHeader()
        {
            if (donorClient.IsLoggedIn)
            {
                var donor = await donorClient.GetAccountAsync();
                DonorPeselTextBlock.Text = donor.Pesel;
            }
            else
            {
                DonorPeselTextBlock.Text = "NONE";
            }
        }

        private async void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            if (!donorClient.IsLoggedIn)
            {
                LoginSnackbar.MessageQueue.Enqueue("You are not logged in");
                LoginSnackbar.MessageQueue.Enqueue("Please login before you logoff");
                return;
            }

            donorClient.Logout();
            LoginSnackbar.MessageQueue.Enqueue("Logged off successfully");
            await SetLoggedDonorsPeselInHeader();
        }
    }
}
