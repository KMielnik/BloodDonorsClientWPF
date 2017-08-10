using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BloodDonorsClientLibrary.Models;
using BloodDonorsClientLibrary.Services;

namespace BloodDonorsClientWPF
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly MiscellaneousClient miscellaneousClient;
        public ObservableCollection<DonorScore> honoraryDonors { get; set; }

        public MainPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            miscellaneousClient = clientFactory.GetMiscellaneousClient();

            honoraryDonors = new ObservableCollection<DonorScore>();

            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var volumeOfBlood = await miscellaneousClient.GetAllBloodDonatedVolumeAsync();
            AmountOfBloodDonatedTextBlock.Text = volumeOfBlood.ToString("n0");

            IEnumerable<DonorScore> honoraryDonorsList = await miscellaneousClient.GetHonoraryDonorsAsync();
            foreach (var donorScore in honoraryDonorsList)
                honoraryDonors.Add(donorScore);
        }
    }
}
