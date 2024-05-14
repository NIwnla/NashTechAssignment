using NashTechAssignmentDay9.Application.Common.Models;
using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface IProjectEmployeeService
{
    IEnumerable<ProjectEmployeeDto> GetAll();
    IEnumerable<ProjectEmployeeDto> GetByConditon(Func<ProjectEmployee, bool> condition);
    Task<bool> CreateAsync(ProjectEmployee projectEmployee);
    Task<bool> UpdateAsync(ProjectEmployee projectEmployee);
    Task<bool> DeleteAsync(Guid projectId, Guid employeeId);
}
