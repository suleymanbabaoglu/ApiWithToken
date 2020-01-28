using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using ApiWithToken.Security.Token;
using System;

namespace ApiWithToken.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly ITokenHandler tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            this.userService = userService;
            this.tokenHandler = tokenHandler;
        }

        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            UserResponse userResponse = userService.FindByEmailandPassword(email, password);
            if (userResponse.Success)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.user);
                return new AccessTokenResponse(accessToken);
            }
            else
            {
                return new AccessTokenResponse(userResponse.Message);
            }
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            UserResponse userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                if (userResponse.user.RefreshTokenEndDate < DateTime.Now)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.user);

                    return new AccessTokenResponse(accessToken);
                }
                else
                {
                    return new AccessTokenResponse("Refresh Token Süresi Dolmuştur...");
                }
            }
            else
            {
                return new AccessTokenResponse("Refresh Token Bulunamadı !!!");
            }
        }

        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            UserResponse userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                userService.RemoveRefreshToken(userResponse.user);
                return new AccessTokenResponse(new AccessToken());
            }
            else
            {
                return new AccessTokenResponse("Refresh Token Bulunamadı");
            }
        }
    }
}