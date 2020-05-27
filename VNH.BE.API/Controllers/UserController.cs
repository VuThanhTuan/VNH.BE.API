using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNH.BE.API.Application.Models;

namespace VNH.BE.API.Controllers
{
    [ApiController]
    [Route("api/users/[action]")]
    public class UserController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetTest()
        {
            var list = new List<UserListModel>()
            {
                new UserListModel() {Id = 1, UserName = "tuan94cntt"},
                new UserListModel() {Id = 2, UserName = "Admin"},
                new UserListModel() {Id = 3, UserName = "Manager"}
            };
            return Ok(list);
        }
    }
}
