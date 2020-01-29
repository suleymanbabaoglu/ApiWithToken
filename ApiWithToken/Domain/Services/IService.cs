using ApiWithToken.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Services
{
    public interface IService<T> where T : class
    {
        public Task<BaseResponse<T>> Add(T param);

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        public Task<BaseResponse<T>> Delete(int id);

        public Task<BaseResponse<T>> GetById(int id);

        public Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate);

        public Task<BaseResponse<T>> Update(T param);
    }
}