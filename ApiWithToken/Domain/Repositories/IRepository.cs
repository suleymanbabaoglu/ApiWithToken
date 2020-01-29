using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T param);

        Task Delete(int id);

        void Update(T param);

        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}