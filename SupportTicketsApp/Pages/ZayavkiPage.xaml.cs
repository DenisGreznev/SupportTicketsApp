using SupportTicketsApp.Data;
using SupportTicketsApp.Models;
using SupportTicketsApp.ViewModels;
using SupportTicketsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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

namespace SupportTicketsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ZayavkiPage.xaml
    /// </summary>
    public partial class ZayavkiPage : Page
    {
        public ZayavkiPage()
        {
            InitializeComponent();
            Loaded += ZayavkiPage_Loaded;

        }
        private async void ZayavkiPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadTicketsAsync(); // Запускаем асинхронную загрузку при загрузке страницы
        }

        private async System.Threading.Tasks.Task LoadTicketsAsync()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                datagrid.ItemsSource = await dbContext.Tickets
                    .Include(t => t.Client)
                    .Include(t => t.Employee)
                    .Include(t => t.Status)
                    .ToListAsync();
            }
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set
            {
                _tickets = value;
                OnPropertyChanged(nameof(Tickets));
            }
        }

       
            
        

      

        


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagrid.SelectedItem is Ticket selectedTicket)
            {
                // Открываем окно с деталями заявки
                TicketDetailsViewModel viewModel = new TicketDetailsViewModel(selectedTicket);
                TicketDetailsWindow detailsWindow = new TicketDetailsWindow(viewModel); // Создаем окно
                detailsWindow.Show(); // Отображаем окно
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int ticketId)
            {
                // 1. Найти заявку по ID
                using (var dbContext = new ApplicationDbContext())
                {
                    Ticket selectedTicket = dbContext.Tickets
                        .Include(t => t.Client)
                        .Include(t => t.Employee)
                        .Include(t => t.Status)
                        .FirstOrDefault(t => t.TicketId == ticketId);

                    if (selectedTicket != null)
                    {
                        // 2. Открыть окно деталей
                        TicketDetailsViewModel viewModel = new TicketDetailsViewModel(selectedTicket);
                        TicketDetailsWindow detailsWindow = new TicketDetailsWindow(viewModel);
                        detailsWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Заявка не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void datagrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
        }
    }
}
