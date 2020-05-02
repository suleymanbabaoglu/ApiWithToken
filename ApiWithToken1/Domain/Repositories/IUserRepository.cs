using ApiWithToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetList();

        void AddUser(User user);

        User FindById(int userId);

        User FindByEmailandPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        User GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(User user);
    }
}