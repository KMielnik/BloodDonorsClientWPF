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

namespace BloodDonorsClientWPF.DonorPages
{
    /// <summary>
    /// Interaction logic for DonorAccountPage.xaml
    /// </summary>
    public partial class DonorAccountPage : Page
    {
        private readonly DonorClient donorClient;

        public DonorAccountPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            donorClient = clientFactory.GetDonorClient();

            Loaded += DonorAccountPage_Loaded;
        }

        private async void DonorAccountPage_Loaded(object sender, RoutedEventArgs e)
        {
            await SetDonorNamerInHeader();
            await SetAmountOfBloodDonated();
            await SetHowManyDaysUntillCanDonateAgain();
            await SetAccountData();
        }

        private async Task SetDonorNamerInHeader()
        {
            var donorName = await donorClient.GetNameAsync();
            DonorNameTextBlockHeader.Text = donorName;
        }

        private async Task SetHowManyDaysUntillCanDonateAgain()
        {
            var whenAbleToDonateAgain = await donorClient.WhenAbleToDonateAgainAsync();

            if (whenAbleToDonateAgain != DateTime.MinValue)
            {
                var howMuchTimeUntil = TimeSpan.Zero;
                HowManyDaysToDonateAgain.Text = howMuchTimeUntil.Days.ToString() + " days";

                HowManyDaysToDonateAgainCalendar.DisplayDate = whenAbleToDonateAgain;
                HowManyDaysToDonateAgainCalendar.SelectedDate = whenAbleToDonateAgain;
            }
            else
            {
                HowManyDaysToDonateAgain.Text = "0 days";
                HowManyDaysToDonateAgainCalendar.DisplayDate = DateTime.Now;
                HowManyDaysToDonateAgainCalendar.SelectedDate = DateTime.Now;
            }


            HowManyDaysToDonateAgainCalendar.DisplayDate = whenAbleToDonateAgain;
            HowManyDaysToDonateAgainCalendar.SelectedDate = whenAbleToDonateAgain;
        }

        private async Task SetAmountOfBloodDonated()
        {
            var amountOfBloodDonated = await donorClient.HowMuchDonatedAsync();
            AmountOfBloodDonatedTextBlock.Text = amountOfBloodDonated.ToString("n0");
        }

        private async Task SetAccountData()
        {
            var donor = await donorClient.GetAccountAsync();
            PeselTextBlock.Text = donor.Pesel;
            PhoneTextBlock.Text = donor.Phone;
            MailTextBlock.Text = donor.Mail;
            BloodTypeTextBlock.Text = $"{donor.BloodType.AboType} Rh{donor.BloodType.RhType}";
        }
    }
}
