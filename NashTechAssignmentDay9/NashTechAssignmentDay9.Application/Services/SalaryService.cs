using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Domain.Entities;
using NashTechAssignmentDay9.Application.Common.Models;

namespace NashTechAssignmentDay9.Application.Services;

public class SalaryService : ISalaryService
{
    private readonly IGenericRepository<Salary> _salaryRepository;
    private readonly IMapper _mapper;
    public SalaryService(IGenericRepository<Salary> genericRepository, IMapper mapper)
    {
        _salaryRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<bool> CreateAsync(Guid employeeId ,SalaryDto salaryDto)
    {
        var salary = _mapper.Map<Salary>(salaryDto);
        salary.EmployeeId = employeeId;
        return await _salaryRepository.CreateAsync(salary);
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var salary = _salaryRepository.FindByCondition(d => d.Id == id).FirstOrDefault();
        return await _salaryRepository.DeleteAsync(salary);
    }

    public IEnumerable<SalaryDto> GetByConditon(Func<Salary, bool> condition)
    {
        var salarys = _salaryRepository.FindByCondition(condition).Include(s => s.Employee);
        var salaryDtos = _mapper.Map<IEnumerable<SalaryDto>>(salarys);
        return salaryDtos;
    }

    public IEnumerable<SalaryDto> GetAll()
    {
        var salarys = _salaryRepository.FindAll().Include(s => s.Employee);
        var salaryDtos = _mapper.Map<IEnumerable<SalaryDto>>(salarys);
        return salaryDtos;
    }

    public async Task<bool> UpdateAsync(Guid id, SalaryDto salaryDto)
    {
        var salary = _mapper.Map<Salary>(salaryDto);
        salary.Id = id;
        return await _salaryRepository.UpdateAsync(salary);
    }
}
