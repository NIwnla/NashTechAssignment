using AutoMapper;
using NashTechAssignmentDay9.Application.Models;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentDto, Department>();
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();
        CreateMap<Employee, EmployeeDto>()
            .ForMember(
                dest => dest.DepartmentName,
                src => src.MapFrom(x => x.Department.Name)
            );
        CreateMap<EmployeeDto, Employee>();
        CreateMap<Salary, SalaryDto>()
            .ForMember(
                dest => dest.EmployeeName,
                src => src.MapFrom(x => x.Employee.Name)
            );
        CreateMap<SalaryDto, Salary>();
        CreateMap<ProjectEmployee, ProjectEmployeeDto>()
            .ForMember(
                dest => dest.ProjectName,
                src => src.MapFrom(x => x.Project.Name)
            ).ForMember(
                dest => dest.EmployeeName,
                src => src.MapFrom(x => x.Employee.Name)
            );
    }
}
