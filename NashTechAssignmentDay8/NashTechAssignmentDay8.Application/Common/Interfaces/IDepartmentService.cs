using NashTechAssignmentDay8.Application.Models;
using NashTechAssignmentDay8.Domain.Entities;

namespace NashTechAssignmentDay8.Application.Common.Interfaces;

public interface IDepartmentService
{
    IEnumerable<DepartmentDto> GetDepartments();
    IEnumerable<DepartmentDto> GetDepartmentsByConditon(Func<Department,bool> condition);
    bool Create (Department department);
    bool Update (Department department);
    bool Delete (Guid id);
}
