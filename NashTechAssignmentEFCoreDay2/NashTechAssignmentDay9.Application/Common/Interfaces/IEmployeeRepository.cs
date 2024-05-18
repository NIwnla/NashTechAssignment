using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    IQueryable<Employee> FindAllWithDepartment();
    IQueryable<Employee> FindByConditionWithDepartment(Func<Employee,bool> condition);
    IQueryable<Employee> FindAllWithProject();
    IQueryable<Employee> FindAllWithSalary();
}
