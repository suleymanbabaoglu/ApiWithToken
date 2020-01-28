using ApiWithToken.Domain.Models;
using ApiWithToken.Security.Token;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly TokenOptions tokenOptions;

        public UserRepository(ApiWithTokenDBContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            this.tokenOptions = tokenOptions.Value;
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
        }

        public User FindByEmailandPassword(string email, string password)
        {
            return context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }

        public User FindById(int userId)
        {
            return context.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        public IEnumerable<User> GetList()
        {
            return context.Users.ToList();
        }

        public User GetUserWithRefreshToken(string refreshToken)
        {
            return context.Users.Where(x => x.RefreshToken == refreshToken).FirstOrDefault();
        }

        public void RemoveRefreshToken(User user)
        {
            User u = this.FindById(user.Id);
            u.RefreshToken = null;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            User u = this.FindById(userId);

            u.RefreshToken = refreshToken;
            u.RefreshTokenEndDate = DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration);
        }
    }
}