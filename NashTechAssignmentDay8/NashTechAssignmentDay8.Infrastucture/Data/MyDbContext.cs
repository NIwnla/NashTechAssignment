using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay8.Domain.Entities;

namespace NashTechAssignmentDay8.Infrastucture.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Salary>().HasOne(salary => salary.Employee).WithOne(employee => employee.Salary);
        modelBuilder.Entity<Employee>().HasOne(employee => employee.Department).WithMany(department => department.Employees).HasForeignKey(employee => employee.DepartmentId);
        modelBuilder.Entity<Project>()
                .HasMany(project => project.Employees)
                .WithMany(employee => employee.Projects)
                .UsingEntity(
                    "ProjectEmployee",
                    e => e.HasOne(typeof(Employee)).WithMany().HasForeignKey("EmployeeId").HasPrincipalKey(nameof(Employee.Id)),
                    p => p.HasOne(typeof(Project)).WithMany().HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id)),
                    t => t.HasKey("EmployeeId", "ProjectId"));
        modelBuilder.Entity<ProjectEmployee>().HasNoKey();
    }
}
