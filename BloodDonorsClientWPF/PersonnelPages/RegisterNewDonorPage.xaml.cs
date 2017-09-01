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
    /// Interaction logic for RegisterNewDonorPage.xaml
    /// </summary>
    public partial class RegisterNewDonorPage : Page
    {
        private readonly PersonnelClient personnelClient;
        private readonly MiscellaneousClient miscellaneousClient;

        public RegisterNewDonorPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            personnelClient = clientFactory.GetPersonnelClient();
            miscellaneousClient = clientFactory.GetMiscellaneousClient();

            Loaded += RegisterNewDonorPage_Loaded;
        }

        private async void RegisterNewDonorPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBloodtypesToCombobox();
            RegisterDonorSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1200));
        }

        private async Task LoadBloodtypesToCombobox()
        {
            IEnumerable<BloodType> bloodTypes = await miscellaneousClient.GetBloodTypesAsync();
            foreach (var bloodType in bloodTypes)
                BloodTypesComboBox.Items.Add(bloodType);
        }

        private async void CreateDonorButton_Click(object sender, RoutedEventArgs e)
        {
            var pesel = PeselTextBox.Text;
            var name = NameTextBox.Text;
            var phone = PhoneTextBox.Text;
            var mail = MailTextBox.Text;
            var bloodType = BloodTypesComboBox.SelectionBoxItem as BloodType;


            try
            {
                await personnelClient.RegisterDonorAsync(pesel, name, bloodType, mail, phone);
            }
            catch (ResouceAlreadyExistsException)
            {
                RegisterDonorSnackbar.MessageQueue.Enqueue("Donor with that pesel already exists");
                return;
            }
            RegisterDonorSnackbar.MessageQueue.Enqueue("Success");
        }
    }
}
