using SupportTicketsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection") // Имя строки подключения из app.config
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Status> Statuses { get; set; }

        // Опционально: Для таблицы RegistrationMethods
        // public DbSet<RegistrationMethod> RegistrationMethods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Здесь можно настроить дополнительные параметры маппинга, если необходимо
            // Например, можно указать составной ключ или изменить имя таблицы

            // Пример:
            // modelBuilder.Entity<Ticket>()
            //     .HasRequired(t => t.Employee)
            //     .WithMany(e => e.Tickets)
            //     .HasForeignKey(t => t.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
