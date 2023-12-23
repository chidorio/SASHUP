using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    [HttpGet("{image}")]
    public ActionResult<byte[]> GetImage(string image)
    {
        try
        {
            var bytes = System.IO.File.ReadAllBytes("Images/"+image);
            Task.Delay(5000).Wait();
            return File(bytes,"image/jpeg");//jpeg
        }
        catch(FileNotFoundException)
        {
            return NotFound();
        }
        catch(Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [Authorize]
    [HttpPost]
    [Route("UploadImage")]
    public async Task<ActionResult<string>> UploadImage([FromForm] IFormFile file)
    {
        if(file.ContentType != "image/png")
            return BadRequest("Неверный формат данных");
        string fileName = Path.GetRandomFileName()+".png";
        try
        {
            using (var stream = new FileStream($"Images/{fileName}",FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(fileName);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
