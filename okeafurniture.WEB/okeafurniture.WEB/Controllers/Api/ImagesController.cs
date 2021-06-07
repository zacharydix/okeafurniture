using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace okeafurniture.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        [HttpPost, Route("add", Name ="AddImage"), Authorize]
        public async Task<IActionResult> AddImageAsync(IFormFile myFile)
        {
            if (myFile == null || myFile.Length == 0)
                return BadRequest("No file added.");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/images",
                        myFile.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await myFile.CopyToAsync(stream);
            }

            return Ok(new { ImageName = myFile.FileName});
        }
    }
}
