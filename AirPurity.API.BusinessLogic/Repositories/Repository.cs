using AirPurity.API.Data.Entities;
using AirPurity.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AirPurity.API.BusinessLogic.Repositories.Interfaces;

namespace AirPurity.API.BusinessLogic.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            if(entity != null)
            {
                _context.Set<T>().Add(entity);
            }
        }

        public void DeleteById(int id)
        {
            var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(
                _context.Set<T>().Where(expression),
                (x, include) => x.Include(include))
                .ToList();
        }

        public virtual ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public ICollection<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(
                _context.Set<T>().AsQueryable(), 
                (x, include) => x.Include(include))
                .ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>()
                .AsQueryable()
                .FirstOrDefault(x => x.Id ==id);
        }

        public T GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(
                _context.Set<T>().AsQueryable(), 
                (x, include) => x.Include(include))
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            if(entity != null)
            {
                _context.Set<T>().Update(entity);
            }
        }
    }
}
