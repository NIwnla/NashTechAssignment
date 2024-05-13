using AutoMapper;
using NashTechAssignmentDay8.Application.Common.Interfaces;
using NashTechAssignmentDay8.Application.Models;
using NashTechAssignmentDay8.Domain.Entities;

namespace NashTechAssignmentDay8.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IGenericRepository<Department> _departmentRepository;
    private readonly IMapper _mapper;
    public DepartmentService(IGenericRepository<Department> genericRepository, IMapper mapper)
    {
        _departmentRepository = genericRepository;
        _mapper = mapper;
    }
    public Task<bool> CreateAsync(Department department) => _departmentRepository.CreateAsync(department);

    public async Task<bool> DeleteAsync(Guid id)
    {
        var department = _departmentRepository.FindByCondition(d => d.Id == id).FirstOrDefault();
        return await _departmentRepository.DeleteAsync(department);
    }

    public  IEnumerable<DepartmentDto> GetDepartmentsByConditon(Func<Department, bool> condition)
    {
        var departments = _departmentRepository.FindByCondition(condition);
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return departmentDtos;
    }

    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
    {
        var departments =  await _departmentRepository.FindAllAsync();
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return departmentDtos;
    }

    public async Task<bool> UpdateAsync(Department department) => await _departmentRepository.UpdateAsync(department);
}
