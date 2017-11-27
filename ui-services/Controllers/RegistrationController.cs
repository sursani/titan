using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Titan.Contexts;
using Titan.Models;
using Titan.Services;

namespace Titan.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;

        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user1 = await _userService.GetUserById(1);
            var user2 = await _userService.GetUserById(2);

            var value = await _userService.DistanceBetweenUsers(user1, user2);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegistrationCreateModel model)
        {
            if (model == null)
                return BadRequest();

            await _userService.RegisterNewUser(model.UserName, model.Password, model.FirstName,
                    model.LastName, model.Email, model.Gender, model.Latitude, model.Longitude);

            return Created("/registration", new { Name = "test" });
        }
    }
}