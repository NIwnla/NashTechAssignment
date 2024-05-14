using NashTechAssignmentDay8.Application.Models;
using NashTechAssignmentDay8.Domain.Entities;

namespace NashTechAssignmentDay8.Application.Common.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
    IEnumerable<DepartmentDto> GetDepartmentsByConditon(Func<Department,bool> condition);
    Task<bool> CreateAsync(Department department);
    Task<bool> UpdateAsync(Department department);
    Task<bool> DeleteAsync(Guid id);
}
