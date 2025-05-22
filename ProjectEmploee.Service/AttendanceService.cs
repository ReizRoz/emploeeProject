using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectEmploee.Core.DTOs;
using ProjectEmploee.Core.Entities;
using ProjectEmploee.Core.Repositories;
using ProjectEmploee.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AttendanseService : IAttendanceService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public AttendanseService(IRepositoryManager repositoryManager, IMapper Mapper)
    {
        _repositoryManager=repositoryManager;
        _mapper = Mapper;
    }


    public async Task<IEnumerable<AttendanceDTO>> GetAllAsync()
    {
        var attendanceAll = await _repositoryManager.Attendance.GetAllAsync();
        var at = _mapper.Map<IEnumerable<AttendanceDTO>>(attendanceAll);
        return at;
    }


    public async Task<AttendanceDTO> GetByIdAsync(int id)
    {
        var attendanceById = await _repositoryManager.Attendance.GetByIdAsync(id);
        var at = _mapper.Map<AttendanceDTO>(attendanceById);
        return at;
    }


    public async Task<AttendanceDTO> PostAsync(Attendance a)
    {
        var attendancePost = await _repositoryManager.Attendance.PostAsync(a);
        var at = _mapper.Map<AttendanceDTO>(attendancePost);
       await _repositoryManager.Save();
        return at;
    }


    public async Task<AttendanceDTO> PutAsync(int id, Attendance a)
    {a.IdAttendance = id;
        var attendancePut = await _repositoryManager.Attendance.PutAsync(id, a);
        if (attendancePut == null)
            throw new Exception($"attendance with ID {id} not found.");
        var at = _mapper.Map<AttendanceDTO>(attendancePut);
        await _repositoryManager.Save();
        return at;
     }

    public async Task DeleteAsync(int id)
    {
        await _repositoryManager.Attendance.DeleteAsync(id);
        await _repositoryManager.Save();
    }
}
