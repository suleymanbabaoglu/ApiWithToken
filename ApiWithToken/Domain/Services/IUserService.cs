using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Services
{
    public interface IUserService
    {
        UserListResponse GetUserList();

        UserResponse AddUser(User user);

        UserResponse FindById(int userId);

        UserResponse FindByEmailandPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        UserResponse GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(User user);
    }
}