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
        BaseResponse<IEnumerable<User>> GetUserList();

        BaseResponse<User> AddUser(User user);

        BaseResponse<User> FindById(int userId);

        BaseResponse<User> FindByEmailandPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        BaseResponse<User> GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(User user);
    }
}