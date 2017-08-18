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

namespace BloodDonorsClientWPF.PersonnelPages
{
    /// <summary>
    /// Interaction logic for PersonnelAccountPage.xaml
    /// </summary>
    public partial class PersonnelAccountPage : Page
    {
        private readonly PersonnelClient personnelClient;

        public PersonnelAccountPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            personnelClient = clientFactory.GetPersonnelClient();

            Loaded += PersonnelAccountPage_Loaded;
        }

        private async void PersonnelAccountPage_Loaded(object sender, RoutedEventArgs e)
        {
            await SetDonorNamerInHeader();
            await SetAccountData();
        }

        private async Task SetDonorNamerInHeader()
        {
            var personnelName = await personnelClient.GetNameAsync();
            PersonnelNameTextBlockHeader.Text = personnelName;
        }

        private async Task SetAccountData()
        {
            var donor = await personnelClient.GetAccountAsync();
            PeselTextBlock.Text = donor.Pesel;
        }
    }
}
