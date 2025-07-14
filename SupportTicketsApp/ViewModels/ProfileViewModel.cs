using SupportTicketsApp.Data;
using SupportTicketsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private Employee _employee;

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        public ProfileViewModel(Employee employee)
        {
            _employee = employee;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
