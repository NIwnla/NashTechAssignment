using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Domain.Entities;

namespace NashTechAssignmentDay9.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Salary>()
                        .HasOne(salary => salary.Employee)
                        .WithOne(employee => employee.Salary)
                        .HasForeignKey<Salary>(salary => salary.EmployeeId)
                        .IsRequired();
                modelBuilder.Entity<Employee>()
                        .HasOne(employee => employee.Department)
                        .WithMany(department => department.Employees)
                        .HasForeignKey(employee => employee.DepartmentId);
                modelBuilder.Entity<Project>()
                        .HasMany(project => project.Employees)
                        .WithMany(employee => employee.Projects)
                        .UsingEntity<ProjectEmployee>();
                modelBuilder.Entity<ProjectEmployee>().HasKey(pe => new { pe.EmployeeId, pe.ProjectId });
        }
}
