using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Repositories;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using ApiWithToken.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public BaseResponse<User> AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
                unitOfWork.CompleteAsync();
                return new BaseResponse<User>(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı Oluşturulurken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public BaseResponse<User> FindByEmailandPassword(string email, string password)
        {
            try
            {
                var user = userRepository.FindByEmailandPassword(email, password);
                if (user == null)
                {
                    return new BaseResponse<User>("Aradığınız Kullanıcı Bulunamadı !!!");
                }
                else
                {
                    return new BaseResponse<User>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı Arama Sırasında Bir Hata Oluştu::{ex.Message}");
            }
        }

        public BaseResponse<User> FindById(int userId)
        {
            try
            {
                var user = userRepository.FindById(userId);
                if (user == null)
                {
                    return new BaseResponse<User>("Aradığınız Kullanıcı Bulunamadı !!!");
                }
                else
                {
                    return new BaseResponse<User>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı Arama Sırasında Bir Hata Oluştu::{ex.Message}");
            }
        }

        public BaseResponse<IEnumerable<User>> GetUserList()
        {
            try
            {
                var userList = userRepository.GetList();
                if (userList.Count() < 1)
                {
                    return new BaseResponse<IEnumerable<User>>("Kullanıcı Bulunmamaktır...");
                }
                else
                {
                    return new BaseResponse<IEnumerable<User>>(userList);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>($"Kullanıcı Listesi Alınırken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public BaseResponse<User> GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                var user = userRepository.GetUserWithRefreshToken(refreshToken);
                if (user == null)
                {
                    return new BaseResponse<User>("Aradığınız Kullanıcı Bulunamadı !!!");
                }
                else
                {
                    return new BaseResponse<User>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>($"Kullanıcı Arama Sırasında Bir Hata Oluştu::{ex.Message}");
            }
        }

        public void RemoveRefreshToken(User user)
        {
            try
            {
                userRepository.RemoveRefreshToken(user);
            }
            catch (Exception ex)
            {
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                userRepository.SaveRefreshToken(userId, refreshToken);
                unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}