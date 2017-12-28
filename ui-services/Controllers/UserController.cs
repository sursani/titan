using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Titan.Services;

namespace Titan.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("UploadPicture")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;

            if (file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    await _userService.UploadPicture(file.FileName, file.ContentType, stream);
                }
            }

            return Ok(new { size });
        }
    }
}