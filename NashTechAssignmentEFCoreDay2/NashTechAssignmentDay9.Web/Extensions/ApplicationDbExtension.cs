using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Infrastructure.Data;

using Newtonsoft.Json;

namespace NashTechAssignmentDay9.Web.Extensions;

public static class ApplicationDbExtension
{
    private static readonly string DATA_FILE_PATH = @"\NashTechAssignmentDay9.Infrastructure\MockData.json";

    public static IApplicationBuilder SeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
            if (!dbContext.Departments.Any())
            {
                // Use this if program failed to read file
                // var departments = new List<Department>{
                //     new Department{Name = "Software Development"},
                //     new Department{Name = "Finance"},
                //     new Department{Name = "Accountant"},
                //     new Department{Name = "HR"}
                // };
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
            if (!dbContext.Projects.Any())
            {
                var projects = new List<Project>{
                    new Project{Name = "Project A"},
                    new Project{Name = "Project B"},
                    new Project{Name = "Project C"},
                    new Project{Name = "Project D"},
                    new Project{Name = "Project E"}
                };
                dbContext.Projects.AddRange(projects);
                dbContext.SaveChanges();
            }
            if (!dbContext.Employees.Any())
            {
                //All employee is in Software Development department
                var employees = new List<Employee>{
                    //In project A, C,D
                    //Salary 50
                    new Employee{Name = "Son",DepartmentId = dbContext.Departments.FirstOrDefault().Id, JoinedDate = DateTime.Now},
                    //Salary 150
                    new Employee{Name = "Khoa",DepartmentId = dbContext.Departments.FirstOrDefault().Id, JoinedDate = DateTime.MinValue},
                    //In project A
                    //Salary 250
                    new Employee{Name = "Thanh",DepartmentId = dbContext.Departments.FirstOrDefault().Id, JoinedDate = DateTime.Now},
                    //Salary 10
                    new Employee{Name = "Hai",DepartmentId = dbContext.Departments.FirstOrDefault().Id, JoinedDate = DateTime.Now},
                    //In project A
                    new Employee{Name = "Binh",DepartmentId = dbContext.Departments.FirstOrDefault().Id, JoinedDate = DateTime.Now}
                };
                dbContext.Employees.AddRange(employees);
                dbContext.SaveChanges();
            }
            if (!dbContext.ProjectEmployees.Any())
            {
                var projectEmployees = new List<ProjectEmployee>{
                    new ProjectEmployee{EmployeeId = dbContext.Employees.FirstOrDefault().Id, ProjectId = dbContext.Projects.FirstOrDefault().Id, Enable = true},
                    new ProjectEmployee{EmployeeId = dbContext.Employees.FirstOrDefault().Id, ProjectId = dbContext.Projects.Skip(2).FirstOrDefault().Id, Enable = true},
                    new ProjectEmployee{EmployeeId = dbContext.Employees.FirstOrDefault().Id, ProjectId = dbContext.Projects.Skip(3).FirstOrDefault().Id, Enable = true},
                    new ProjectEmployee{EmployeeId = dbContext.Employees.Skip(2).FirstOrDefault().Id, ProjectId = dbContext.Projects.FirstOrDefault().Id, Enable = true},
                    new ProjectEmployee{EmployeeId = dbContext.Employees.Skip(4).FirstOrDefault().Id, ProjectId = dbContext.Projects.FirstOrDefault().Id, Enable = true}
                };
                dbContext.ProjectEmployees.AddRange(projectEmployees);
                dbContext.SaveChanges();
            }
            if (!dbContext.Salaries.Any())
            {
                var salaries = new List<Salary>{
                    new Salary{EmployeeId = dbContext.Employees.FirstOrDefault().Id, Amount = 50},
                    new Salary{EmployeeId = dbContext.Employees.Skip(1).FirstOrDefault().Id, Amount = 150},
                    new Salary{EmployeeId = dbContext.Employees.Skip(2).FirstOrDefault().Id, Amount = 250},
                    new Salary{EmployeeId = dbContext.Employees.Skip(3).FirstOrDefault().Id, Amount = 10}
                };
                dbContext.Salaries.AddRange(salaries);
                dbContext.SaveChanges();
            }
            return app;
        }

    }
}
