using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.Common;
using SBTestTask.Common.Infrastructure.RabbitMq;
using SBTestTask.Common.Models;
using SBTestTask.WebApi.Helpers.RabbitMq;

namespace SBTestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRabbitQueue _queue;

        public UserController(IRabbitQueue queue, IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _queue = queue;

            _queue.Setup(rabbitMqConfiguration.Get(), Constants.RabbitQueueName);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateUser(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest();
                }

                _queue.Publish(Constants.RabbitQueueName, Encoding.UTF8.GetBytes(username));
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}