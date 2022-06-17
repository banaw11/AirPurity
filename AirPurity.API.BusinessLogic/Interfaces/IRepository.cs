using System.Linq.Expressions;

namespace AirPurity.API.BusinessLogic.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        abstract T GetById(int id);
        T GetById(int id, params Expression<Func<T, object>>[] includes);
        ICollection<T> GetAll();
        ICollection<T> GetAll(params Expression<Func<T, object>>[] includes);
        abstract ICollection<T> FindAll(Expression<Func<T, bool>> expression);
        ICollection<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        void DeleteById(int id);
        void Add(T entity);
        void Update(T entity);
        void SaveChanges();

    }
}
