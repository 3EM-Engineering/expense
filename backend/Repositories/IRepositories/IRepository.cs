using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repositories.IRepositories
{
    public interface IRepository<T>
    {
        public T? Get(int Id);
        public IEnumerable<T>? GetByFilter(Expression<Func<T, bool>> filter);
        public IEnumerable<T>? GetAll();
        public void Add(T obj);
        public void Update(T obj);
        public void Delete(T obj);
        public void DeleteByFilter(Expression<Func<T, bool>> filter);
        public void Save();
        public void SaveAsync();
    }
}
