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

        public UserResponse AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
                unitOfWork.CompleteAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı Oluşturulurken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public UserResponse FindByEmailandPassword(string email, string password)
        {
            try
            {
                var user = userRepository.FindByEmailandPassword(email, password);
                if (user == null)
                {
                    return new UserResponse("Aradığınız Kullanıcı Bulunamadı !!!");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı Arama Sırasında Bir Hata Oluştu::{ex.Message}");
            }
        }

        public UserResponse FindById(int userId)
        {
            try
            {
                var user = userRepository.FindById(userId);
                if (user == null)
                {
                    return new UserResponse("Aradığınız Kullanıcı Bulunamadı !!!");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı Arama Sırasında Bir Hata Oluştu::{ex.Message}");
            }
        }

        public UserListResponse GetUserList()
        {
            try
            {
                var userList = userRepository.GetList();
                if (userList.Count() < 1)
                {
                    return new UserListResponse("Kullanıcı Bulunmamaktır...");
                }
                else
                {
                    return new UserListResponse(userList);
                }
            }
            catch (Exception ex)
            {
                return new UserListResponse($"Kullanıcı Listesi Alınırken Bir Hata Oluştu::{ex.Message}");
            }
        }

        public UserResponse GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                var user = userRepository.GetUserWithRefreshToken(refreshToken);
                if (user == null)
                {
                    return new UserResponse("Aradığınız Kullanıcı Bulunamadı !!!");
                }
                else
                {
                    return new UserResponse(user);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı Arama Sırasında Bir Hata Oluştu::{ex.Message}");
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