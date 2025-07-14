using SupportTicketsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SupportTicketsApp.ViewModels
{
    public class TicketDetailsViewModel : INotifyPropertyChanged
    {
        private Ticket _ticket;

        public TicketDetailsViewModel(Ticket ticket)
        {
            _ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
            // Здесь можно выполнить дополнительные действия, например, загрузить связанные данные
            // или преобразовать данные для отображения.
        }

        public int TicketId => _ticket.TicketId;
        public string Title => _ticket.Title;
        public string Description => _ticket.Description;
        public string ClientName => _ticket.Client?.ClientName; // Используем навигационные свойства
        public string EmployeeFullName => _ticket.Employee?.FullName;
        public string StatusName => _ticket.Status?.StatusName;

        public Brush RowColor => GetRowColor(_ticket.Status?.StatusName);

        private Brush GetRowColor(string statusName)
        {
            switch (statusName)
            {
                case "Открыта":
                    return Brushes.Green;
                case "В работе":
                    return Brushes.Yellow;
                case "Завершена":
                    return Brushes.LightGreen;
                case "Отклонена":
                    return Brushes.Red;
                default:
                    return Brushes.White; // Или любой цвет по умолчанию
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
