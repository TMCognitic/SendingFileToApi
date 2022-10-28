using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostFile(FileContent form)
        {
            byte[] content = Convert.FromBase64String(form.Content);

            return Ok();
        }
    }
}
