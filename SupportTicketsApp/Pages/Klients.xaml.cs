using SupportTicketsApp.Data;
using SupportTicketsApp.Views;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupportTicketsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Klients.xaml
    /// </summary>
    public partial class Klients : Page
    {
        public Klients()
        {
            InitializeComponent();
            LoadKlients();
        }

        private void LoadKlients()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                datagrid.ItemsSource = dbContext.Clients.ToList();
            }
        }

        private void buttondobklient_Click(object sender, RoutedEventArgs e)
        {
            // 1. Затемняем фон
            this.IsEnabled = false; // Отключаем главное окно (это делает его неактивным)
            // Можно использовать эффект размытия, но это может снижать производительность:
            // Effect = new BlurEffect() { Radius = 10 };
            Effect = new BlurEffect() { Radius = 10 };
            // 2. Создаем и показываем окно ZayavkaAdd как модальное
            KlientsAdd klientAddWindow = new KlientsAdd();
            klientAddWindow.Owner = Application.Current.MainWindow; //  Устанавливаем владельца, чтобы окно было модальным

            bool? result = klientAddWindow.ShowDialog(); // ShowDialog - модальный режим

            // 3.  После закрытия ZayavkaAddWindow
            this.IsEnabled = true;  // Включаем главное окно обратно
            //  Удаляем эффект размытия, если он был
            // Effect = null;
            Effect = null;
            // 4.  Обработка результата, если необходимо (например, если ZayavkaAddWindow
            //    возвращает результат, указывающий на успешное сохранение данных)
            if (result == true)
            {
                // Обработка успешного сохранения (если применимо)
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
