namespace NashTechAssignmentDay9.Application.Common.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> FindAll();
   
    IQueryable<T> FindByCondition(Func<T, bool> condition);
    Task<bool> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);

    Task<bool> SaveAsync();
    // T1 GetShadowProperty<T1>(string propertyName, T entity);
}
