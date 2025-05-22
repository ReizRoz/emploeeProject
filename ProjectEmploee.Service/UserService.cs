using AutoMapper;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Repositories;
using ProjectEmploee.Core.Services;
using Microsoft.EntityFrameworkCore;


namespace ProjectEmploee.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper Mapper, IRepositoryManager repositoryManager)
        {
            _userRepository = userRepository;
            _mapper = Mapper;
            _repositoryManager = repositoryManager;
        }


        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var userAll = await _userRepository.GetAllAsync(query =>
        query.Include(u => u.Attendances));//לא עשיתי ונקלוד
            var us = _mapper.Map<IEnumerable<UserDTO>>(userAll);
            return us;
        }


        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var userById = await _userRepository.GetByIdAsync(id);
            var us = _mapper.Map<UserDTO>(userById);
            return us;
        }


        public async Task<User> GetByNameAsync(string name)
        {
            var userByName = await _userRepository.GetByNameAsync(name);
            return userByName;
        }


        public async Task<UserDTO> PostAsync(User u)
        {
            var userPost = await _userRepository.PostAsync(u);
            await _repositoryManager.Save();
            var us = _mapper.Map<UserDTO>(userPost);
            return us;
        }


        public async Task<UserDTO> PutAsync(int id, User u)
        {
            u.IdUser = id;
            var existing = await _userRepository.PutAsync(id, u);
            if (existing == null)
                throw new Exception($"User with ID {id} not found.");
            await _repositoryManager.Save();
            return _mapper.Map<UserDTO>(existing);
        }


        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            await _repositoryManager.Save();
        }
    }
}



