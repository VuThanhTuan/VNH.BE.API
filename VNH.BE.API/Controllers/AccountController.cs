using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNH.BE.API.Application.Models.Users;
using VNH.BE.API.Infrastructure.Services;

namespace VNH.BE.API.Controllers
{
    [ApiController]
    [Route("api/account/[action]")]
    public class AccountController: Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _accountService.Signin(userLogin.UserName, userLogin.Password);

            if(user == null)
            {
                return BadRequest("Invalid username or password");
            }

            var token = _accountService.CreateToken(user);

            return Ok(token);
        }
    }
}
