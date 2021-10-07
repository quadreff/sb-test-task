using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.Common.Models;

namespace SBTestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateUser([FromBody]User user)
        {
            return Ok();
        }
    }
}