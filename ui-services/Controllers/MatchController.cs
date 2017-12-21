using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Titan.Services;

namespace Titan.Controllers
{
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<ValuesController> _logger;

        public MatchController(IUserService userService, ILogger<ValuesController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int userId1, int userId2)
        {
            var user1 = await _userService.GetUserById(userId1);
            var user2 = await _userService.GetUserById(userId2);

            var value = await _userService.DistanceBetweenUsers(user1, user2);
            return Ok(value);
        }
    }
}