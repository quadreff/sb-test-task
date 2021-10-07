using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.WebApi.App.Validation;
using SBTestTask.WebApi.Helpers.Tokens.Jwt;
using SBTestTask.WebApi.Models;

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
        public IActionResult GenerateToken([FromBody]AuthInfo authInfo)
        {
            try
            {
                // simple validation is performed in Validate method
                _validationService.Validate(authInfo);
                return Ok(_tokenManager.GenerateToken(authInfo.Username).AsString());
            }
            catch (UnauthorizedException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
