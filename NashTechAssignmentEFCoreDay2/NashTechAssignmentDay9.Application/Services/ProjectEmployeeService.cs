using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Common.Models;
using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Application.Services;

public class ProjectEmployeeService : IProjectEmployeeService
{
    private readonly IGenericRepository<ProjectEmployee> _projectEmployeeRepository;
    private readonly IMapper _mapper;
    public ProjectEmployeeService(IGenericRepository<ProjectEmployee> genericRepository, IMapper mapper)
    {
        _projectEmployeeRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateAsync(ProjectEmployee projectEmployee)
    {
        return await _projectEmployeeRepository.CreateAsync(projectEmployee);
    }

    public async Task<bool> DeleteAsync(Guid projectId, Guid employeeId)
    {
        var projectEmployeeToDelete = _projectEmployeeRepository.FindByCondition(x => x.ProjectId == projectId && x.EmployeeId == employeeId).FirstOrDefault();
        if (projectEmployeeToDelete == null){
            return false;
        }
        return await _projectEmployeeRepository.DeleteAsync(projectEmployeeToDelete);
    }

    public IEnumerable<ProjectEmployeeDto> GetAll()
    {
       var projectEmployees = _projectEmployeeRepository.FindAll().Include(x => x.Project).Include(x => x.Employee).AsEnumerable();
       var projectEmployeeDtos = _mapper.Map<IEnumerable<ProjectEmployeeDto>>(projectEmployees);
       return projectEmployeeDtos;
    }

    public IEnumerable<ProjectEmployeeDto> GetByConditon(Func<ProjectEmployee, bool> condition)
    {
        var projectEmployees = _projectEmployeeRepository.FindByCondition(condition).Include(x => x.Project).Include(x => x.Employee).AsEnumerable();
        var projectEmployeeDtos = _mapper.Map<IEnumerable<ProjectEmployeeDto>>(projectEmployees);
        return projectEmployeeDtos;
    }

    public async Task<bool> UpdateAsync(ProjectEmployee projectEmployee)
    {
        return await _projectEmployeeRepository.UpdateAsync(projectEmployee);
    }

}
