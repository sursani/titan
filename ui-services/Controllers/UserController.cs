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

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            long size = file.Length;
            
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }



            return Ok(new { size, filePath});
        }

        [HttpGet]
        public async Task<IActionResult> GetAction()
        {
            var str = await _userService.UploadPictures("");

            return Ok(new { result = str });
        }
    }
}