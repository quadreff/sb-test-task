using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.Common.Logging;
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
        public IActionResult GenerateToken([FromBody] AuthInfo authInfo)
        {
            try
            {
                // simple validation is performed in Validate method
                _validationService.Validate(authInfo);

                Log.Trace($"Validation passed, generating token for {authInfo.Name}");
                return Ok(_tokenManager.GenerateToken(authInfo.Name).AsString());
            }
            catch (UnauthorizedException)
            {
                Log.Error($"Validation for user failed {authInfo.Name}");
                return Unauthorized();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}