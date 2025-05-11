using ProjectEmploee.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Entities
{
    public class Attendance
    { [Key]
     public int IdAttendance { get; set; }
        [ForeignKey("idUser")]
     public int UserId { get; set; }
     public DateTime Arrival { get; set; }
     public DateTime? Departure { get; set; }
     public User User { get; set; }
    }
}
