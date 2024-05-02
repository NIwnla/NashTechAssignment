using Microsoft.OpenApi.Models;
using NashTechAssignmentDay3.Middleware;

namespace NashTechAssignmentDay3
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test", Version = "v1" });
			});
			var app = builder.Build();

			app.MapGet("/", () => "Hello World!");

			app.UseLogginRequest();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
			});

			app.Run();
		}
	}
}
