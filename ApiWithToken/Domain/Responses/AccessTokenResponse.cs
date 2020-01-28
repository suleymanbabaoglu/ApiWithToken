using ApiWithToken.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Responses
{
    public class AccessTokenResponse : BaseResponse
    {
        public AccessToken accessToken { get; set; }

        public AccessTokenResponse(bool success, string message, AccessToken accessToken) : base(success, message)
        {
        }

        public AccessTokenResponse(AccessToken accessToken) : this(true, string.Empty, accessToken)
        {
        }

        public AccessTokenResponse(string message) : this(false, message, null)
        {
        }
    }
}