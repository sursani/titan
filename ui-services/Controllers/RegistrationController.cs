using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Titan.Contexts;
using Titan.Data;
using Titan.Models;
using Titan.Services;

namespace Titan.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<ValuesController> _logger;

        public RegistrationController(IUserService userService,
                                      ILogger<ValuesController> logger)
        {
            _userService = userService;
            _logger = logger;
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