using NashTechAssignmentDay9.Application.Models;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAll();
    IEnumerable<EmployeeDto> GetByConditon(Func<Employee, bool> condition);
    Task<bool> CreateAsync(Guid deparmentId, EmployeeDto employee);
    Task<bool> UpdateAsync(Guid id, EmployeeDto employee);
    Task<bool> DeleteAsync(Guid id);

    IEnumerable<Employee> GetAllWithProjects();
    IEnumerable<Employee> GetAllWithDepartment();
    IEnumerable<Employee> GetAllWithSalary();
}
