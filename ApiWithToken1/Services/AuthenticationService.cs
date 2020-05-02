using ApiWithToken.Domain.Models;
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

        public BaseResponse<AccessToken> CreateAccessToken(string email, string password)
        {
            BaseResponse<User> userResponse = userService.FindByEmailandPassword(email, password);
            if (userResponse.Success)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.Extra);
                return new BaseResponse<AccessToken>(accessToken);
            }
            else
            {
                return new BaseResponse<AccessToken>(userResponse.ErrorMessage);
            }
        }

        public BaseResponse<AccessToken> CreateAccessTokenByRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                if (userResponse.Extra.RefreshTokenEndDate < DateTime.Now)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.Extra);

                    return new BaseResponse<AccessToken>(accessToken);
                }
                else
                {
                    return new BaseResponse<AccessToken>("Refresh Token Süresi Dolmuştur...");
                }
            }
            else
            {
                return new BaseResponse<AccessToken>("Refresh Token Bulunamadı !!!");
            }
        }

        public BaseResponse<AccessToken> RevokeRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                userService.RemoveRefreshToken(userResponse.Extra);
                return new BaseResponse<AccessToken>(new AccessToken());
            }
            else
            {
                return new BaseResponse<AccessToken>("Refresh Token Bulunamadı");
            }
        }
    }
}