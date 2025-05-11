using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Data.Repository
{

    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        public IRepository<Attendance> Attendance { get; }
        public IRepository<Request> Request { get; }
        public IUserRepository User { get; }



        public RepositoryManager(DataContext context, IRepository<Attendance> AttendanceRepository,
            IRepository<Request> RequestRepository, IUserRepository userRepository)
        {
            _context = context;
            Attendance = AttendanceRepository;
            Request = RequestRepository;
            User = userRepository;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

