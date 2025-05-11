using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEmploee.Core.Repositories
{
    public interface IRepository<T> where T : class//לבדוק מה זה אומר
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);

        Task<T?> GetByIdAsync(int id);

        Task<T> PostAsync(T entity);

        Task<T> PutAsync(int id, T entity);

        Task DeleteAsync(int id);
    }
}
