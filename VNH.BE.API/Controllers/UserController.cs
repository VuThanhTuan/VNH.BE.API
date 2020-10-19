using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VNH.BE.API.Infrastructure.ActionResult;
using VNH.BE.Domain.Aggregates.Identity;
using VNH.BE.Infrastructure.Repositories;

namespace VNH.BE.API.Controllers
{
    [ApiController]
    [Route("api/users/[action]")]
    public class UserController : Controller
    {
        private readonly IApplicationUserRepository _repository;
        public UserController(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUser(int pageSize, int pageIndex)
        {
            var test = HttpContext.User;
            var list = _repository.GetAllUser(pageSize, pageIndex);
            var total = list.Count();
            var response = new JsonResponse<List<ApplicationUser>>(statusCode: StatusCodes.Status200OK,
                pageSize: pageSize, pageIndex: pageIndex, count: total, data: list);
            return Ok(response);
        }
    }
}
