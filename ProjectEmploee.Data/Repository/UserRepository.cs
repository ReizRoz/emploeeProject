using Microsoft.EntityFrameworkCore;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Data.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
       public async Task<User> GetByNameAsync(string name)
        {
            var u = await _dbSet.FirstOrDefaultAsync(u => u.Name == name);
            return u;
        }
    }
}
