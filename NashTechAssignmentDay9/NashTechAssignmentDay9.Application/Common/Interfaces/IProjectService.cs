using NashTechAssignmentDay9.Application.Models;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface IProjectService
{
    IEnumerable<ProjectDto> GetAll();
    IEnumerable<ProjectDto> GetByConditon(Func<Project, bool> condition);
    Task<bool> CreateAsync(ProjectDto projectDto);
    Task<bool> UpdateAsync(Guid id, ProjectDto projectDto);
    Task<bool> DeleteAsync(Guid id);
}
