using Microsoft.AspNetCore.Mvc;
using Titan.Models;

namespace Titan.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        [HttpPost]
        public IActionResult Create([FromBody] RegistrationCreateModel model)
        {
            if (model == null)
                return BadRequest();

            return Created("/registration", new { Name = model.FirstName });
        }
    }
}