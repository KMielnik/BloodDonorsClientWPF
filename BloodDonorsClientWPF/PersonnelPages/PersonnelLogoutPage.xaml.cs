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
    /// Interaction logic for PersonnelLogoutPage.xaml
    /// </summary>
    public partial class PersonnelLogoutPage : Page
    {
        private readonly PersonnelClient personnelClient;

        public PersonnelLogoutPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            personnelClient = clientFactory.GetPersonnelClient();
        }
    }
}
