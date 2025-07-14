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

namespace SupportTicketsApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }

            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdZayavki_Click(object sender, RoutedEventArgs e)
        {
            // PagesNavigation.Navigate(new HomePage());

            PagesNavigation.Navigate(new System.Uri("Pages/ZayavkiPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdKlients_Click(object sender, RoutedEventArgs e)
        {
            // PagesNavigation.Navigate(new HomePage());

            PagesNavigation.Navigate(new System.Uri("Pages/KlientsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdSotrudniki_Click(object sender, RoutedEventArgs e)
        {
            // PagesNavigation.Navigate(new HomePage());

            PagesNavigation.Navigate(new System.Uri("Pages/SotrudnikiPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdOtch_Click(object sender, RoutedEventArgs e)
        {
            // PagesNavigation.Navigate(new HomePage());

            PagesNavigation.Navigate(new System.Uri("Pages/OtchPage.xaml", UriKind.RelativeOrAbsolute));
        }
        private void home_Loaded_1(object sender, RoutedEventArgs e)
        {
            btnMenu.IsChecked = true;
            PagesNavigation.Navigate(new System.Uri("Pages/ZayavkiPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void home_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();

            }
            else
            {
                e.Cancel = true;
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void buttondob_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonprofile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
