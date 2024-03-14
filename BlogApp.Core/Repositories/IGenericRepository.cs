using System.Linq.Expressions;

namespace BlogApp.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task CreateAsync(T entity);

        public void Update(T entity);
        public  Task Delete(T entity);
        public Task<List<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        public Task<T> GetByIdAsync(int id);
        public IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
