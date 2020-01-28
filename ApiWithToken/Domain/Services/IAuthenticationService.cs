using ApiWithToken.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithToken.Domain.Services
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string email, string password);

        AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken);

        AccessTokenResponse RevokeRefreshToken(string refreshToken);
    }
}