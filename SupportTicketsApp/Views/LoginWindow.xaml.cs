using SupportTicketsApp.Data;
using SupportTicketsApp.Models;
using SupportTicketsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;


namespace SupportTicketsApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Login = LoginTextBox.Text;
            _viewModel.Password = PasswordBox.Password;
           

            // Проверка подключения к базе данных
            if (!IsDatabaseConnected())
            {
                MessageBox.Show("Не удалось установить соединение с базой данных. Проверьте настройки подключения.", "Ошибка подключения", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем процесс авторизации, если нет соединения
            }


            using (var dbContext = new ApplicationDbContext())
            {
                if (_viewModel.LoginCommand(dbContext))
                {
                    // Авторизация успешна
                    Employee employee = dbContext.Employees.FirstOrDefault(emp => emp.Login == _viewModel.Login); // Получаем информацию о сотруднике

                    if (employee != null)
                    {
                        if (employee.Role == "Администратор")
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                        }
                        else if (employee.Role == "Ведущий специалист")
                        {
                            EmployeeDashboardWindow dashboardWindow = new EmployeeDashboardWindow(employee.EmployeeId);
                            dashboardWindow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Неизвестная роль пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        Close(); // Закрываем окно авторизации
                    }
                    else
                    {
                        MessageBox.Show("Ошибка получения информации о сотруднике.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Отображаем сообщение об ошибке
                    MessageBox.Show(_viewModel.ErrorMessage, "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Метод для проверки подключения к базе данных
        private bool IsDatabaseConnected()
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    // Попытка открыть соединение с базой данных
                    dbContext.Database.Connection.Open();
                    dbContext.Database.Connection.Close(); // Закрываем соединение сразу после открытия

                    return true; // Соединение успешно установлено
                }
            }
            catch (SqlException ex)
            {
                // Обработка ошибок подключения
                Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
                return false; // Соединение не установлено
            }
        }
    }
}
