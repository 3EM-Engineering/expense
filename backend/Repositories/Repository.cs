using System.Linq.Expressions;
using backend.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _appDbContext;
        public DbSet<T> _dbSet;
        public Repository(
                ApplicationDbContext appDbContext
            )
        {
            _appDbContext = appDbContext;
            this._dbSet = _appDbContext.Set<T>();
        }
        public void Add(T obj)
        {
            _dbSet.Add(obj);
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public void DeleteByFilter(Expression<Func<T, bool>> filter)
        {
            var listItem = _dbSet.Where(filter).ToList();
            if(listItem.Any()) _dbSet.RemoveRange(listItem);
        }

        public T? Get(int Id)
        {
            return _dbSet.Find(Id);
        }

        public IEnumerable<T>? GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T>? GetByFilter(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void SaveAsync()
        {
            _appDbContext.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }
    }
}
