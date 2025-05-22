using ProjectEmploee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Repositories
{
    public interface IRepositoryManager
    {
        IRepository<Attendance> Attendance { get; }

        IRepository<Request> Request { get; }

        IUserRepository User { get; }

        Task Save();
    }
}
