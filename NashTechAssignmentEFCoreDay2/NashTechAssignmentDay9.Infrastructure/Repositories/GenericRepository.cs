using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Application.Common.Interfaces;
using NashTechAssignmentDay9.Infrastructure.Data;
namespace NashTechAssignmentDay9.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    public GenericRepository(ApplicationDbContext dbContext)
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

    public IQueryable<T> FindAll()
    {
        return _dbContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Func<T, bool> condition)
    {
        using (var tranaction = _dbContext.Database.BeginTransaction())
        {
            var result = _dbContext.Set<T>().Where(condition).AsQueryable();
			tranaction.Commit();
            return result;
        }
    }

    public async Task<bool> SaveAsync() => await _dbContext.SaveChangesAsync() > 0;
    public async Task<bool> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return await SaveAsync();
    }

}
