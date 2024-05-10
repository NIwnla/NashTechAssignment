namespace NashTechAssignmentDay8.Application.Common.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Func<T, bool> condition);
    bool Create(T entity);
    bool Update(T entity);
    bool Delete(T entity);

    bool Save();
    // T1 GetShadowProperty<T1>(string propertyName, T entity);
}
