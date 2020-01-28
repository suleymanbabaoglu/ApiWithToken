using ApiWithToken.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Responses
{
    public class UserListResponse : BaseResponse
    {
        public IEnumerable<User> userList { get; set; }

        private UserListResponse(bool success, string message, IEnumerable<User> userList) : base(success, message)
        {
            this.userList = userList;
        }

        //BAŞARILI
        public UserListResponse(IEnumerable<User> userList) : this(true, string.Empty, userList)
        {
        }

        //BAŞARISIZ
        public UserListResponse(string message) : this(false, message, null)
        {
        }
    }
}