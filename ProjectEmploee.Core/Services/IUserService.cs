using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();

        Task<UserDTO> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);

        Task<UserDTO> PostAsync(User u);

        Task<UserDTO> PutAsync(int id, User u);

        Task DeleteAsync(int id);
    }
}
