using NashTechAssignmentDay8.Domain.Entities;
using NashTechAssignmentDay8.Infrastructure.Data;

using Newtonsoft.Json;

namespace NashTechAssignmentDay8.Web.Extensions;

public static class ApplicationDbExtension
{
    private static readonly string DATA_FILE_PATH = @"\NashTechAssignmentDay8.Infrastructure\MockData.json";

    public static IApplicationBuilder SeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<MyDbContext>();
            dbContext.Database.EnsureCreated();
            if (!dbContext.Departments.Any())
            {
                var departments = new List<Department>();
                var relativePath = Directory.GetParent(Environment.CurrentDirectory).FullName;
                string fullPath = relativePath + DATA_FILE_PATH;
                if (File.Exists(fullPath))
                {
                    using (StreamReader r = new StreamReader(fullPath))
                    {
                        string json = r.ReadToEnd();
                        departments = JsonConvert.DeserializeObject<List<Department>>(json);
                    }
                    dbContext.Departments.AddRange(departments);
                    dbContext.SaveChanges();
                }
            }
            return app;
        }

    }
}
