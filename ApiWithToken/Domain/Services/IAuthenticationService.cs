using ApiWithToken.Domain.Responses;
using ApiWithToken.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Services
{
    public interface IAuthenticationService
    {
        BaseResponse<AccessToken> CreateAccessToken(string email, string password);

        BaseResponse<AccessToken> CreateAccessTokenByRefreshToken(string refreshToken);

        BaseResponse<AccessToken> RevokeRefreshToken(string refreshToken);
    }
}