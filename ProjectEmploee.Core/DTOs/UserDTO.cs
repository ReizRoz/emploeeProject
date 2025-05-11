using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Attendance> Attendances { get; set; }

    }
}
