using SupportTicketsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.ViewModels
{
    public class LKViewModel : INotifyPropertyChanged
    {
        private Employee _employee;

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Метод для сохранения изменений
        public void SaveChanges()
        {
            // TODO: Реализуйте логику сохранения данных в базу данных или другой источник.
            // Используйте _employee.Login, _employee.FullName и т.д. для получения измененных данных.

            // После сохранения можно обновить UI, если это необходимо
            // OnPropertyChanged(nameof(Employee));
        }
    }
}
