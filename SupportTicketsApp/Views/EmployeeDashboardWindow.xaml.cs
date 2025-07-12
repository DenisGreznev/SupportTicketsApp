using SupportTicketsApp.Data;
using SupportTicketsApp.ViewModels;
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
using System.Windows.Shapes;

namespace SupportTicketsApp.Views
{
    public partial class EmployeeDashboardWindow : Window
    {
        public EmployeeDashboardWindow(int employeeId)
        {
            InitializeComponent();

            // Получаем экземпляр ApplicationDbContext
            using (var dbContext = new ApplicationDbContext())
            {
                DataContext = new EmployeeDashboardViewModel(employeeId, dbContext);
            }
        }
    }
}
