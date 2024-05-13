using NashTechAssignmentDay9.Application.Models;
using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface IDepartmentService
{
    IEnumerable<DepartmentDto> GetAll();
    IEnumerable<DepartmentDto> GetByConditon(Func<Department,bool> condition);
    Task<bool> CreateAsync(DepartmentDto department);
    Task<bool> UpdateAsync(Guid id,DepartmentDto department);
    Task<bool> DeleteAsync(Guid id);
}
