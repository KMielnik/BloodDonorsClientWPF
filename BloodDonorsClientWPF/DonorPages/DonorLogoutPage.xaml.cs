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

namespace BloodDonorsClientWPF.DonorPages
{
    /// <summary>
    /// Interaction logic for DonorLogoutPage.xaml
    /// </summary>
    public partial class DonorLogoutPage : Page
    {
        private readonly DonorClient donorClient;

        public DonorLogoutPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            donorClient = clientFactory.GetDonorClient();
        }
    }
}
