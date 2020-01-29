using ApiWithToken.Domain.Repositories;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using ApiWithToken.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiWithToken.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> repository;
        private readonly IUnitOfWork unitOfWork;

        public Service(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<T>> Add(T param)
        {
            try
            {
                await repository.Add(param);
                await unitOfWork.CompleteAsync();
                return new BaseResponse<T>(param);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await repository.CountWhere(predicate);
        }

        public async Task<BaseResponse<T>> Delete(int id)
        {
            try
            {
                T t = await repository.GetById(id);
                if (t != null)
                {
                    await repository.Delete(id);
                    await unitOfWork.CompleteAsync();

                    return new BaseResponse<T>(t);
                }
                else
                {
                    return new BaseResponse<T>("Id'ye ait kayıt bulunamadı...");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<T>> GetById(int id)
        {
            try
            {
                T t = await repository.GetById(id);
                if (t != null)
                {
                    return new BaseResponse<T>(t);
                }
                else
                {
                    return new BaseResponse<T>("Id ye ait kayıt bulunamadı...");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> t = await repository.GetWhere(predicate);
            return new BaseResponse<IEnumerable<T>>(t);
        }

        public async Task<BaseResponse<T>> Update(T param)
        {
            try
            {
                repository.Update(param);
                await unitOfWork.CompleteAsync();
                return new BaseResponse<T>(param);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }
    }
}