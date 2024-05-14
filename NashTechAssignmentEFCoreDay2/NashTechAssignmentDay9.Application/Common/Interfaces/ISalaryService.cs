using NashTechAssignmentDay9.Application.Models;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface ISalaryService
{
    IEnumerable<SalaryDto> GetAll();
    IEnumerable<SalaryDto> GetByConditon(Func<Salary,bool> condition);
    Task<bool> CreateAsync(Guid employeeId ,SalaryDto salary);
    Task<bool> UpdateAsync(Guid id,SalaryDto salary);
    Task<bool> DeleteAsync(Guid id);
}
