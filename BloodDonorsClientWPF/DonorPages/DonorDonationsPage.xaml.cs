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
    /// Interaction logic for DonorDonationsPage.xaml
    /// </summary>
    public partial class DonorDonationsPage : Page
    {
        private readonly DonorClient donorClient;

        public DonorDonationsPage(ClientFactory clientFactory)
        {
            InitializeComponent();
            donorClient = clientFactory.GetDonorClient();
        }
    }
}
