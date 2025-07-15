using SupportTicketsApp.Models;
using SupportTicketsApp.Pages;
using SupportTicketsApp.ViewModels;
using SupportTicketsApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Profile;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
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
        public Employee LoggedInEmployee { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded; // Подписываемся на событие Loaded
            textlogin.Text = UserSession.LoggedInUser;
        }
       
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoggedInEmployee != null)
            {
                textlogin.Text = LoggedInEmployee.Login;
            }
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
            PagesNavigation.Navigate(new System.Uri("Pages/ZayavkiPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdKlients_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/Klients.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdSotrudniki_Click(object sender, RoutedEventArgs e)
        {

            PagesNavigation.Navigate(new System.Uri("Pages/SotrPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdOtch_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/ExcelPage.xaml", UriKind.RelativeOrAbsolute));
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
            // 1. Затемняем фон
            this.IsEnabled = false; // Отключаем главное окно (это делает его неактивным)
            Effect = new BlurEffect() { Radius = 10 };
            // 2. Создаем и показываем окно ZayavkaAdd как модальное
            ZayavkaAdd zayavkaAddWindow = new ZayavkaAdd();
            zayavkaAddWindow.Owner = this; //  Устанавливаем владельца, чтобы окно было модальным

            bool? result = zayavkaAddWindow.ShowDialog(); // ShowDialog - модальный режим
            
            // 3.  После закрытия ZayavkaAddWindow
            this.IsEnabled = true;  // Включаем главное окно обратно
            //  Удаляем эффект размытия
            Effect = null;
            if (result == true)
            {
            }
        }
        public static class EmployeeContext
        {
            public static Employee CurrentEmployee { get; set; }
        }

        private void buttonprofile_Click(object sender, RoutedEventArgs e)
        {

            PagesNavigation.Navigate(new System.Uri("Pages/lk.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdlk_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/lk.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
