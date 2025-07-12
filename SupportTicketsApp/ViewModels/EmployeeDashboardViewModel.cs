using SupportTicketsApp.Data;
using SupportTicketsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.ViewModels
{
    public class EmployeeDashboardViewModel
    {
        public ObservableCollection<Ticket> Tickets { get; set; }

        public EmployeeDashboardViewModel(int employeeId, ApplicationDbContext dbContext)
        {
            // Загружаем заявки для данного сотрудника из базы данных
            Tickets = new ObservableCollection<Ticket>(dbContext.Tickets
                .Where(t => t.EmployeeId == employeeId)
                .ToList());
        }
    }
}
