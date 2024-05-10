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
    public bool Create(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        return Save();
    }

    public bool Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Save();
    }

    public IQueryable<T> FindAll() => _dbContext.Set<T>();


    public IQueryable<T> FindByCondition(Func<T, bool> condition) => _dbContext.Set<T>().Where(condition).AsQueryable();

    public bool Save() => _dbContext.SaveChanges() > 0 ? true : false;


    public bool Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return Save();
    }

}
