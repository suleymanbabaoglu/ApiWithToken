using ApiWithToken.Domain.Resources;
using ApiWithToken.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using ApiWithToken.Domain.Extensions;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Security.Token;

namespace ApiWithToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                BaseResponse<AccessToken> accessTokenResponse = authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);
                if (accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.Extra);
                }
                else
                {
                    return BadRequest(accessTokenResponse.ErrorMessage);
                }
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            BaseResponse<AccessToken> accessTokenResponse = authenticationService.CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.Extra);
            }
            else
            {
                return BadRequest(accessTokenResponse.ErrorMessage);
            }
        }

        [HttpPost]
        public IActionResult RemoveRefreshToken(TokenResource tokenResource)
        {
            BaseResponse<AccessToken> accessTokenResponse = authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.Extra);
            }
            else
            {
                return BadRequest(accessTokenResponse.ErrorMessage);
            }
        }
    }
}