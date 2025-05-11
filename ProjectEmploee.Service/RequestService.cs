using AutoMapper;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Repositories;
using ProjectEmploee.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectEmploee.Data.Repository;

namespace ProjectEmploee.Service
{
    public class RequestService : IRequestService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IRepository<Request> _attendanceRepository;
        private readonly IMapper _mapper;
        public RequestService(IRepositoryManager repositoryManager, IMapper Mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = Mapper;
        }
        public async Task<IEnumerable<RequestDTO>> GetAllAsync()
        {
            var requestAll = await _repositoryManager.Request.GetAllAsync(//query =>
        //query.Include(r=>r.User)
        );//לא עשיתי ונקלוד
            var re = _mapper.Map<IEnumerable<RequestDTO>>(requestAll);
            return re;
        }


        public async Task<RequestDTO> GetByIdAsync(int id)
        {
            var requestById = await _repositoryManager.Request.GetByIdAsync(id);
            var re = _mapper.Map<RequestDTO>(requestById);
            return re;
        }


        public async Task<RequestDTO> PostAsync(Request r)
        {
            var requestPost = await _repositoryManager.Request.PostAsync(r);
            await _repositoryManager.Save();
            var re = _mapper.Map<RequestDTO>(requestPost);
            return re;
        }


        public async Task<RequestDTO> PutAsync(int id, Request r)
        {
            var requestPut = await _repositoryManager.Request.PutAsync(id, r);
            var re = _mapper.Map<RequestDTO>(requestPut);
            return re;

        }

        public async Task DeleteAsync(int id)
        {
            await _repositoryManager.Request.DeleteAsync(id);
            await _repositoryManager.Save();
        }
    }
}
