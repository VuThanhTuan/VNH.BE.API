using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VNH.BE.API.Controllers
{
    [ApiController]
    [Route("users/[action]")]
    public class UserController : Controller
    {
        public IActionResult GetTest()
        {
            return Ok("Test");
        }
    }
}
