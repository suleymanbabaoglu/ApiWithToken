using ApiWithToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Responses
{
    public class UserResponse : BaseResponse
    {
        public User user { get; set; }

        private UserResponse(bool success, string message, User user) : base(success, message)
        {
            this.user = user;
        }

        //BAŞARILI OLURSA DÖNECEK OLAN RESPONSE
        public UserResponse(User user) : this(true, string.Empty, user)
        {
        }

        //BAŞARISIZ OLURSA DÖNECEK OLAN RESPONSE
        public UserResponse(string message) : this(false, message, null)
        {
        }
    }
}