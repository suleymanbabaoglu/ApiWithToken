using ApiWithToken.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApiWithTokenDBContext context;
        private DbSet<T> table = null;

        public Repository(ApiWithTokenDBContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task Add(T param)
        {
            await table.AddAsync(param);
        }

        public async Task<int> CountWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await table.CountAsync(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        public async Task Delete(int id)
        {
            T t = await GetById(id);
            table.Remove(t);
        }

        public void Update(T param)
        {
            context.Entry(param).State = EntityState.Modified;
        }
    }
}