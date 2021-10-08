using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTestTask.Common;
using SBTestTask.Common.Infrastructure;
using SBTestTask.Common.Infrastructure.RabbitMq;
using SBTestTask.WebApi.Helpers.RabbitMq;

namespace SBTestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRabbitQueue _queue;
        private readonly IUserRepository _repository;

        public UserController(IRabbitQueue queue, IRabbitMqConfiguration rabbitMqConfiguration,
            IUserRepository repository)
        {
            _queue = queue;
            _repository = repository;

            _queue.Setup(rabbitMqConfiguration.Get(), Constants.RabbitQueueName);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _repository.GetAllAsync());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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