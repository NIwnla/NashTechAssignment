using NashTechAssignmentDay7.Application.Common.Interfaces;
using NashTechAssignmentDay7.Application.Repositories;
using NashTechAssignmentDay7.Application.Services;

namespace NashTechAssignmentDay7.Web.Extension
{
	public static class ServiceExtension
	{
		public static IServiceCollection AddRepositoryAndService(this IServiceCollection services) 
		{
			services.AddScoped<IPersonRepository, PersonRepository>();
			services.AddScoped<IRookiesService, RookiesService>();
			return services;
		}
	}
}
