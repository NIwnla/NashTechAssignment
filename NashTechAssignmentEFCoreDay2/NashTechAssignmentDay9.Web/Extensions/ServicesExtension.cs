using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Application.Services;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Infrastructrure.Repositories;
using NashTechAssignmentDay9.Infrastructure.Repositories;

namespace NashTechAssignmentDay9.Web.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IGenericRepository<Project>, GenericRepository<Project>>();
        services.AddScoped<IGenericRepository<Salary>, GenericRepository<Salary>>();
        services.AddScoped<IGenericRepository<ProjectEmployee>, GenericRepository<ProjectEmployee>>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ISalaryService, SalaryService>();
        services.AddScoped<IProjectEmployeeService, ProjectEmployeeService>();
        return services;
    }
}
