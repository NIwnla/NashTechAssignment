using AutoMapper;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IGenericRepository<Project> _projectRepository;
    private readonly IMapper _mapper;
    public ProjectService(IGenericRepository<Project> genericRepository, IMapper mapper)
    {
        _projectRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateAsync(ProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        return await _projectRepository.CreateAsync(project);
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var project = _projectRepository.FindByCondition(d => d.Id == id).FirstOrDefault();
        return await _projectRepository.DeleteAsync(project);
    }

    public IEnumerable<ProjectDto> GetByConditon(Func<Project, bool> condition)
    {
        var projects = _projectRepository.FindByCondition(condition);
        var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return projectDtos;
    }

    public IEnumerable<ProjectDto> GetAll()
    {
        var projects = _projectRepository.FindAll();
        var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return projectDtos;
    }

    public async Task<bool> UpdateAsync(Guid id, ProjectDto projectDto)
    {
        var project = _mapper.Map<Project>(projectDto);
        project.Id = id;
        return await _projectRepository.UpdateAsync(project);
    }


}
