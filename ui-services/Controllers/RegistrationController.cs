using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Titan.Contexts;
using Titan.Models;

namespace Titan.Controllers
{
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly TitanContext _context;

        public RegistrationController(TitanContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegistrationCreateModel model)
        {
            if (model == null)
                return BadRequest();

            var users = await _context.Users.ToListAsync();

            return Created("/registration", new { Name = users.First().FirstName });
        }
    }
}