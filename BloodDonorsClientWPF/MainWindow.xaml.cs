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

namespace BloodDonorsClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClientFactory clientFactory;

        public MainWindow()
        {
            InitializeComponent();
            clientFactory = new ClientFactory();
            MainFrame.Content = new MainPage(clientFactory);
        }

        private void ShowServersAvailability(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            
        }

        private void ExitButtonClicked(object sender, RoutedEventArgs e)
            => Close();
        

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
