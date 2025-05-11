using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDTO>> GetAllAsync();

        Task<AttendanceDTO> GetByIdAsync(int id);

        Task<AttendanceDTO> PostAsync(Attendance a);

        Task<AttendanceDTO> PutAsync(int id, Attendance a);

        Task DeleteAsync(int id);
    }
}
