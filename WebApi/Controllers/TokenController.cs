using Microsoft.AspNetCore.Mvc;
using SBTestTask.WebApi.App.Validation;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;

namespace SBTestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _tokenManager;
        private readonly IValidationService _validationService;

        public TokenController(IJwtTokenManager tokenManager, IValidationService validationService)
        {
            _tokenManager = tokenManager;
            _validationService = validationService;
        }

        [HttpPost]
        public IActionResult GenerateToken(string username, string password)
        {
            return Ok();
        }
    }
}
