using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiWithToken.Domain.Extensions;
using ApiWithToken.Domain.Models;
using ApiWithToken.Domain.Resources;
using ApiWithToken.Domain.Responses;
using ApiWithToken.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUserList()
        {
            BaseResponse<IEnumerable<User>> userListResponse = userService.GetUserList();

            if (userListResponse.Success)
            {
                return Ok(userListResponse.Extra);
            }
            else
            {
                return BadRequest(userListResponse.ErrorMessage);
            }
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            BaseResponse<User> userResponse = userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int userId)
        {
            BaseResponse<User> userResponse = userService.FindById(userId);
            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }

        [HttpGet("{refreshToken:string}")]
        public IActionResult GetUserByRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }

        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                User user = mapper.Map<UserResource, User>(userResource);
                BaseResponse<User> userResponse = userService.AddUser(user);

                if (userResponse.Success)
                {
                    return Ok(userResponse.Extra);
                }
                else
                {
                    return BadRequest(userResponse.ErrorMessage);
                }
            }
        }
    }
}