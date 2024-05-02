using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NashTechAssignmentDay3.Model;
using System.Text;
using System.Threading.Tasks;

namespace NashTechAssignmentDay3.Middleware
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class LogginRequest
	{
		private readonly RequestDelegate _next;

		public LogginRequest(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			//httpContext.Request.Body return Stream so use StreamReader to turn it into string
			string bodyString = "";
			using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
			{
				bodyString = await reader.ReadToEndAsync();
			}

			LogginInformationModel logginInformation = new LogginInformationModel
			{
				Host = httpContext.Request.Host,
				Schema = httpContext.Request.Scheme,
				Path = httpContext.Request.Path,
				QueryString = httpContext.Request.QueryString,
				RequestBody = bodyString
			};

			logginInformation.WriteObjectToFile();		
			await _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class LogginRequestExtensions
	{
		public static IApplicationBuilder UseLogginRequest(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<LogginRequest>();
		}
	}
}
