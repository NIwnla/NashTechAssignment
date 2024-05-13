using AutoMapper;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Models;
using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IGenericRepository<Department> _departmentRepository;
    private readonly IMapper _mapper;
    public DepartmentService(IGenericRepository<Department> genericRepository, IMapper mapper)
    {
        _departmentRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateAsync(DepartmentDto departmentDto)
    {
        var department = _mapper.Map<Department>(departmentDto);
        return await _departmentRepository.CreateAsync(department);
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var department = _departmentRepository.FindByCondition(d => d.Id == id).FirstOrDefault();
        return await _departmentRepository.DeleteAsync(department);
    }

    public IEnumerable<DepartmentDto> GetByConditon(Func<Department, bool> condition)
    {
        var departments = _departmentRepository.FindByCondition(condition);
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return departmentDtos;
    }

    public IEnumerable<DepartmentDto> GetAll()
    {

            var departments = _departmentRepository.FindAll().ToList();
            var departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);
            return departmentDtos;

    }

    public async Task<bool> UpdateAsync(Guid id, DepartmentDto departmentDto)
    {
        var department = _mapper.Map<Department>(departmentDto);
        department.Id = id;
        return await _departmentRepository.UpdateAsync(department);
    }
}
