using SupportTicketsApp.Data;
using SupportTicketsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.ViewModels
{
    public class LoginViewModel
    {
        private string _login;
        private string _password;
        private string _errorMessage;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool LoginCommand(ApplicationDbContext dbContext)
        {
            ErrorMessage = "";

            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Пожалуйста, заполните все поля.";
                return false;
            }

            // 1. Ищем сотрудника в базе данных по логину и паролю
            Employee employee = dbContext.Employees.FirstOrDefault(e => e.Login == Login && e.Password == Password);

            // 2. Проверяем, найден ли сотрудник
            if (employee != null)
            {
                // Авторизация успешна
                return true;
            }
            else
            {
                // Неверный логин или пароль
                ErrorMessage = "Неверный логин или пароль.";
                return false;
            }
        }
    }
}
