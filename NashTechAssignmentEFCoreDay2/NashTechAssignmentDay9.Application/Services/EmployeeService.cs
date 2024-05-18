using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;
namespace NashTechAssignmentDay9.Application.Services;

public class EmployeeService : IEmployeeService
{
	private readonly IEmployeeRepository _employeeRepository;
	private readonly IGenericRepository<Department> _departmentRepository;
	private readonly IGenericRepository<Salary> _salaryRepository;
	private readonly IMapper _mapper;
	public EmployeeService(IEmployeeRepository employeeRepository,
							IGenericRepository<Department> departmentRepository,
							IGenericRepository<Salary> salaryRepository,
							IMapper mapper)
	{
		_employeeRepository = employeeRepository;
		_departmentRepository = departmentRepository;
		_salaryRepository = salaryRepository;
		_mapper = mapper;
	}
	public async Task<bool> CreateAsync(Guid departmentId, EmployeeDto employeeDto)
	{
		var department = _departmentRepository.FindByCondition(d => d.Id == departmentId).FirstOrDefault();
		if (department == null)
		{
			return false;
		}
		var employee = _mapper.Map<Employee>(employeeDto);
		employee.DepartmentId = departmentId;
		if (!await _employeeRepository.CreateAsync(employee))
		{
			return false;
		}
		var salary = new Salary
		{
			EmployeeId = employee.Id,
			Amount = 0
		};
		return await _salaryRepository.CreateAsync(salary);
	}
	public async Task<bool> DeleteAsync(Guid id)
	{
		var employee = _employeeRepository.FindByCondition(d => d.Id == id).FirstOrDefault();
		return await _employeeRepository.DeleteAsync(employee);
	}

	public IEnumerable<EmployeeDto> GetByConditon(Func<Employee, bool> condition)
	{
		var employees = _employeeRepository.FindByConditionWithDepartment(condition);
		var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
		return employeeDtos;
	}

	public IEnumerable<EmployeeDto> GetAll()
	{
		var employees = _employeeRepository.FindAllWithDepartment();
		var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
		return employeeDtos;
	}

	public async Task<bool> UpdateAsync(Guid id, EmployeeDto employeeDto)
	{
		var employee = _employeeRepository.FindByCondition(e => e.Id == id).FirstOrDefault();
		if (employee == null)
		{
			return false;
		}
		employee.Name = employeeDto.Name;
		employee.JoinedDate = employeeDto.JoinedDate;
		var updatedDepartment = _departmentRepository.FindByCondition(e => e.Name.Equals(employeeDto.DepartmentName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
		if (updatedDepartment != null) employee.DepartmentId = updatedDepartment.Id;
		return await _employeeRepository.UpdateAsync(employee);
	}

	public IEnumerable<Employee> GetAllWithDepartment()
	{
		return _employeeRepository.FindAllWithDepartment();
	}

	public IEnumerable<Employee> GetAllWithProjects()
	{
		return _employeeRepository.FindAllWithProject();
	}

	public IEnumerable<Employee> GetAllWithSalary()
	{
		return _employeeRepository.FindAllWithSalary();
	}

}
