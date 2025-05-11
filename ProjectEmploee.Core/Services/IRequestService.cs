using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestDTO>> GetAllAsync();

        Task<RequestDTO> GetByIdAsync(int id);

        Task<RequestDTO> PostAsync(Request r);

        Task<RequestDTO> PutAsync(int id, Request r);

        Task DeleteAsync(int id);
    }
}
