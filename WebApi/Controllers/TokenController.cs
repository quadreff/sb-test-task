using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.Common.Models;
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
        public IActionResult GenerateToken([FromBody]User user)
        {
            try
            {
                // simple validation is performed in Validate method
                _validationService.Validate(user);
                return Ok(_tokenManager.GenerateToken(user.Name).AsString());
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
