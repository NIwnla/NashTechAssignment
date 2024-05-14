using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Infrastructure.Data;

namespace NashTechAssignmentDay9.Infrastructrure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MyDbContext _dbContext;
    public EmployeeRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(Employee entity)
    {
        _dbContext.Set<Employee>().Add(entity);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(Employee entity)
    {
        _dbContext.Set<Employee>().Remove(entity);
        return await SaveAsync();
    }

    public IQueryable<Employee> FindAll()
    {
        return _dbContext.Set<Employee>();
    }

    public IQueryable<Employee> FindAllWithDepartment()
    {
        // var employees = _dbContext.Set<Employee>().Include(x => x.Department);
        using (var tranaction = _dbContext.Database.BeginTransaction())
        {
            var employees = _dbContext.Set<Employee>().ToList();
            var departments = _dbContext.Set<Department>().ToList();
            var query = from employee in employees
                        join department in departments on employee.DepartmentId equals department.Id
                        select new Employee
                        {
                            DepartmentId = employee.DepartmentId,
                            Id = employee.Id,
                            JoinedDate = employee.JoinedDate,
                            Department = department,
                            Name = employee.Name
                        };

            tranaction.Commit();
            return query.AsQueryable();
        }

    }

    public IQueryable<Employee> FindAllWithProject()
    {
        // var employees = _dbContext.Set<Employee>().Include(e => e.Projects);
        var employees = _dbContext.Set<Employee>();
        var projects = _dbContext.Set<Project>();
        var projectEmployees = _dbContext.Set<ProjectEmployee>();
        var query = from employee in employees
                    join projectEmployee in projectEmployees on employee.Id equals projectEmployee.EmployeeId into proEmpGroup
                    from proj in proEmpGroup.DefaultIfEmpty()
                    join project in projects on proj.ProjectId equals project.Id into projectGroup
                    select new Employee
                    {
                        DepartmentId = employee.DepartmentId,
                        Id = employee.Id,
                        JoinedDate = employee.JoinedDate,
                        Projects = projectGroup.ToList(),
                        Name = employee.Name
                    };
        return query.AsQueryable();
    }

    public IQueryable<Employee> FindAllWithSalaryMore100AndJoinedDateMore20240101() // IDK how many naming conventions I violated
    {
        var dateTime = new DateTime(2024,1,1);
        // var employees = _dbContext.Set<Employee>().Include(e => e.Salary).Where(e => e.JoinedDate > dateTime && e.Salary.Amount > 100);
        var employees = _dbContext.Set<Employee>().ToList();
        var salaries = _dbContext.Set<Salary>().ToList();
        var query = from employee in employees
                    join salary in salaries on employee.Id equals salary.EmployeeId
                    where salary.Amount > 100 && employee.JoinedDate > dateTime
                    select new Employee
                    {
                        DepartmentId = employee.DepartmentId,
                        Id = employee.Id,
                        JoinedDate = employee.JoinedDate,
                        Salary = salary,
                        Name = employee.Name
                    };
        return query.AsQueryable();
    }


    public IQueryable<Employee> FindByCondition(Func<Employee, bool> condition)
    {
        return _dbContext.Set<Employee>().Where(condition).AsQueryable();
    }

    public IQueryable<Employee> FindByConditionWithDepartment(Func<Employee, bool> condition)
    {
        return _dbContext.Set<Employee>().Where(condition).AsQueryable().Include(e => e.Department);
    }


    public async Task<bool> SaveAsync() => await _dbContext.SaveChangesAsync() > 0;
    public async Task<bool> UpdateAsync(Employee entity)
    {
        _dbContext.Set<Employee>().Update(entity);
        return await SaveAsync();
    }

}
