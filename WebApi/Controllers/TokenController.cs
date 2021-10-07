using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.WebApi.Helpers.Tokens;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;

namespace SBTestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _tokenManager;

        public TokenController(IJwtTokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public IActionResult GenerateToken(string username, string password)
        {
            return Ok();
        }
    }
}
