using System;
using System.Collections;
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

namespace BloodDonorsClientWPF.PersonnelPages
{
    /// <summary>
    /// Interaction logic for PersonnelDonationsPage.xaml
    /// </summary>
    public partial class PersonnelDonationsPage : Page
    {
        private readonly PersonnelClient personnelClient;
        private readonly MiscellaneousClient miscellaneousClient;

        private ObservableCollection<BloodDonation> Donations { get; set; }

        public ObservableCollection<BloodDonation> FilteredDonations { get; set; }

        public PersonnelDonationsPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            personnelClient = clientFactory.GetPersonnelClient();
            miscellaneousClient = clientFactory.GetMiscellaneousClient();

            Donations = new ObservableCollection<BloodDonation>();
            FilteredDonations = new ObservableCollection<BloodDonation>();

            Loaded += PersonnelDonationsPage_Loaded;
        }

        private async void PersonnelDonationsPage_Loaded(object sender, RoutedEventArgs e)
        {
            IEnumerable<BloodType> bloodTypes = await miscellaneousClient.GetBloodTypesAsync();
            IEnumerable<BloodDonation> donations = await personnelClient.GetAllBloodAsync();

            foreach (var bloodType in bloodTypes)
                BloodTypeComboBox.Items.Add(bloodType);

            foreach (var donation in donations)
                Donations.Add(donation);
            FilterDataGrid();
        }


        private void FilterGrid_KeyUp(object sender, KeyEventArgs e)
        {
            FilterDataGrid();
        }

        private void BloodTypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BloodTypeComboBox.SelectedItem = null;
            DonorPeselTextBox.Text = "";
            BloodTakerTextBox.Text = "";

            FilterDataGrid();
        }

        private void FilterDataGrid()
        {
            IEnumerable<BloodDonation> temporaryFilteredDonations = Donations.AsEnumerable();

            var bloodType = BloodTypeComboBox.SelectedItem;
            var donorPesel = DonorPeselTextBox.Text;
            var bloodTakerPesel = BloodTakerTextBox.Text;

            if (bloodType != null)
                temporaryFilteredDonations =
                    temporaryFilteredDonations.Where(x => x.BloodType.ToString() == bloodType.ToString());
            if (donorPesel != "")
                temporaryFilteredDonations =
                    temporaryFilteredDonations.Where(x => x.Donor.Pesel.Contains(donorPesel));
            if (bloodTakerPesel != "")
                temporaryFilteredDonations =
                    temporaryFilteredDonations.Where(x => x.BloodTaker.Pesel.Contains(bloodTakerPesel));

            FilteredDonations.Clear();
            foreach (var temporaryFilteredDonation in temporaryFilteredDonations)
                FilteredDonations.Add(temporaryFilteredDonation);
        }
    }
}
