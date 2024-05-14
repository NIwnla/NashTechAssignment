using AutoMapper;
using NashTechAssignmentDay8.Application.Models;
using NashTechAssignmentDay8.Domain.Entities;

namespace NashTechAssignmentDay8.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile(){
        CreateMap<Department, DepartmentDto>();
    }
}
