using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketsApp.Models
{
    [Table("Statuses")]
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; }

        // Навигационное свойство: список заявок с этим статусом
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
