using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Infrastructure.Data;
using NashTechAssignmentDay9.Infrastructure.Repositories;

namespace NashTechAssignmentDay9.Infrastructrure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
	private readonly ApplicationDbContext _dbContext;
	public EmployeeRepository(ApplicationDbContext dbContext)
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
		using (var tranaction = _dbContext.Database.BeginTransaction())
		{
			var query = @"
        SELECT e.*, d.Id as DId, d.Name as DName
        FROM Employees e
        INNER JOIN Departments d ON e.DepartmentId = d.Id";

			var employeesWithDepartments = _dbContext.Employees
				.FromSqlRaw(query).Include(e => e.Department);
			tranaction.Commit();
			return employeesWithDepartments.AsQueryable();
		}

	}

	public IQueryable<Employee> FindAllWithProject()
	{
		var query = @"
        SELECT e.*, p.Id as ProjectId, p.Name as ProjectName
        FROM Employees e
        LEFT JOIN ProjectEmployees pe ON e.Id = pe.EmployeeId
        LEFT JOIN Projects p ON pe.ProjectId = p.Id";
		var employeesWithProjects = _dbContext.Employees.FromSqlRaw(query).Include(e => e.Projects);
		return employeesWithProjects.AsQueryable();
	}

	public IQueryable<Employee> FindAllWithSalary()
	{
		var query = @"
			SELECT e.*, s.Amount
			FROM Employees e
			inner join Salaries s on e.Id = s.EmployeeId
			WHERE s.Amount > 100 and e.JoinedDate > '01/01/2024'";
		var employeeWithSalary = _dbContext.Employees.FromSqlRaw(query).Include(e => e.Salary);
		return employeeWithSalary.AsQueryable();
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
