using ProjectEmploee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetByNameAsync(string name);
    }
}
