using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.Models
{
    public class Employee
    {
        [Key] // Указывает, что это первичный ключ
        public int EmployeeId { get; set; }

        [Required] // Указывает, что поле обязательно для заполнения
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Dolgnost { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Telefon { get; set; }


        // Навигационное свойство: список заявок, назначенных этому сотруднику
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
