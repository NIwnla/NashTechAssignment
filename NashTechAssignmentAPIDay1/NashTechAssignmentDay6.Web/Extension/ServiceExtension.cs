using NashTechAssignmentDay6.Application.Common.Interfaces;
using NashTechAssignmentDay6.Application.Repositories;
using NashTechAssignmentDay6.Application.Services;

namespace NashTechAssignmentDay6.Web.Extension
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddRepositoryAndService(this IServiceCollection services) 
		{
			services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();
			services.AddScoped<IWorkTaskService, WorkTaskService>();
			return services;
		}
	}
}
