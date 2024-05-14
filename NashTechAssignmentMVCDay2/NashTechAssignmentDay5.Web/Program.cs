using NashTechAssignmentDay5.Application.Interfaces;
using NashTechAssignmentDay5.Application.Services;
using NashTechAssignmentDay5.Application.Repositories;

namespace NashTechAssignmentDay5
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IPersonRepository, PersonRepository>();
			builder.Services.AddScoped<IRookiesService, RookiesService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.MapControllerRoute(
				name: "MyArea",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
