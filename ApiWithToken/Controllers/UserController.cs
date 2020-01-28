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
            UserListResponse userListResponse = userService.GetUserList();

            if (userListResponse.Success)
            {
                return Ok(userListResponse.userList);
            }
            else
            {
                return BadRequest(userListResponse.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            UserResponse userResponse = userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int userId)
        {
            UserResponse userResponse = userService.FindById(userId);
            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }
            else
            {
                return BadRequest(userResponse.Message);
            }
        }

        [HttpGet("{refreshToken:string}")]
        public IActionResult GetUserByRefreshToken(string refreshToken)
        {
            UserResponse userResponse = userService.GetUserWithRefreshToken(refreshToken);
            if (userResponse.Success)
            {
                return Ok(userResponse.user);
            }
            else
            {
                return BadRequest(userResponse.Message);
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
                UserResponse userResponse = userService.AddUser(user);

                if (userResponse.Success)
                {
                    return Ok(userResponse.user);
                }
                else
                {
                    return BadRequest(userResponse.Message);
                }
            }
        }
    }
}