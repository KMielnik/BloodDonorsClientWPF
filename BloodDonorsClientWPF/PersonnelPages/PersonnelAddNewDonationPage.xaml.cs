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
using BloodDonorsClientLibrary.Models;
using BloodDonorsClientLibrary.Services;
using MaterialDesignThemes.Wpf;

namespace BloodDonorsClientWPF.PersonnelPages
{
    /// <summary>
    /// Interaction logic for PersonnelAddNewDonationPage.xaml
    /// </summary>
    public partial class PersonnelAddNewDonationPage : Page
    {
        private readonly PersonnelClient personnelClient;

        public PersonnelAddNewDonationPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            personnelClient = clientFactory.GetPersonnelClient();

            Loaded += PersonnelAddNewDonationPage_Loaded;
        }

        private void PersonnelAddNewDonationPage_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterDonorSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1200));
        }

        private async void AddDonationButton_Click(object sender, RoutedEventArgs e)
        {
            var pesel = PeselTextBox.Text;
            var result = int.TryParse(VolumeTextBox.Text,out int volume);

            if (result == false)
            {
                RegisterDonorSnackbar.MessageQueue.Enqueue("PLEASE, enter a correct volume.");
                return;
            }

            var donationDate = DonationDatePicker.SelectedDate ?? DateTime.Now;
            var donationTime = DonationTimePicker.SelectedTime ?? DateTime.Now;

            var donationDateTime = new DateTime(donationDate.Year,donationDate.Month,donationDate.Day,
                                                donationTime.Hour,donationTime.Minute,donationTime.Second);

            try
            {
                await personnelClient.AddDonationAsync(donationDateTime, volume, pesel);
            }
            catch (Exception)
            {
                RegisterDonorSnackbar.MessageQueue.Enqueue("Error");
                return;
            }

            RegisterDonorSnackbar.MessageQueue.Enqueue("Success");
        }
    }
}
