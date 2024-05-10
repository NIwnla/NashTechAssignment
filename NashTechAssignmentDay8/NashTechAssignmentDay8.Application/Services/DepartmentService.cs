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
    public bool Create(Department department) => _departmentRepository.Create(department);

    public bool Delete(Guid id)
    {
        var department = _departmentRepository.FindByCondition(d => d.Id == id).FirstOrDefault();
        return _departmentRepository.Delete(department);
    }

    public IEnumerable<DepartmentDto> GetDepartmentsByConditon(Func<Department, bool> condition)
    {
        var departments = _departmentRepository.FindByCondition(condition);
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return departmentDtos;
    }

    public IEnumerable<DepartmentDto> GetDepartments()
    {
        var departments = _departmentRepository.FindAll();
        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return departmentDtos;
    }

    public bool Update(Department department) => _departmentRepository.Update(department);

}
