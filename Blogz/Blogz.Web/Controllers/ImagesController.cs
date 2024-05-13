using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {

            return Ok("");
        }
    }
}
