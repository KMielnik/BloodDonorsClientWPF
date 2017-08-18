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

namespace BloodDonorsClientWPF.PersonnelPages
{
    /// <summary>
    /// Interaction logic for PersonnelLoginPage.xaml
    /// </summary>
    public partial class PersonnelLoginPage : Page
    {
        private readonly PersonnelClient personnelClient;

        public PersonnelLoginPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            personnelClient = clientFactory.GetPersonnelClient();

            Loaded += LoginPage_Loaded;
            LoginSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1200));
        }

        private async void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            await SetLoggedPersonnelPeselInHeader();
        }

        private async void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var pesel = PeselTextBox.Text;
            if (pesel == PersonnelPeselTextBlock.Text)
            {
                LoginSnackbar.MessageQueue.Enqueue("You are already logged in");
            }

            var password = PasswordTextBox.Password;

            try
            {
                await personnelClient.LoginAsync(pesel, password);
            }
            catch (InvalidLoginCredentialsException)
            {
                LoginSnackbar.MessageQueue.Enqueue("Invalid credentials");
                return;
            }

            var donorName = await personnelClient.GetNameAsync();
            LoginSnackbar.MessageQueue.Enqueue("You have been successfully logged in");
            LoginSnackbar.MessageQueue.Enqueue($"Welcome {donorName}");
            await SetLoggedPersonnelPeselInHeader();
        }

        private async Task SetLoggedPersonnelPeselInHeader()
        {
            if (personnelClient.IsLoggedIn)
            {
                var donor = await personnelClient.GetAccountAsync();
                PersonnelPeselTextBlock.Text = donor.Pesel;
            }
            else
            {
                PersonnelPeselTextBlock.Text = "NONE";
            }
        }

        private async void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            if (!personnelClient.IsLoggedIn)
            {
                LoginSnackbar.MessageQueue.Enqueue("You are not logged in");
                LoginSnackbar.MessageQueue.Enqueue("Please login before you logoff");
                return;
            }

            personnelClient.Logout();
            LoginSnackbar.MessageQueue.Enqueue("Logged off successfully");
            await SetLoggedPersonnelPeselInHeader();
        }

        private void LoginCard_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    LoginButtonClick(this, e);
                    break;
            }
        }
    }
}
