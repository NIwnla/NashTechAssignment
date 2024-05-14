using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay8.Application.Common.Interfaces;
using NashTechAssignmentDay8.Infrastructure.Data;
namespace NashTechAssignmentDay8.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly MyDbContext _dbContext;
    public GenericRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return await SaveAsync();
    }

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public IEnumerable<T> FindByCondition(Func<T, bool> condition)
    {
        return _dbContext.Set<T>().Where(condition);
    }
    public async Task<bool> SaveAsync() => await _dbContext.SaveChangesAsync() > 0;
    public async Task<bool> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return await SaveAsync();
    }

}
