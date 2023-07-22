using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("/api/upload")]
    public class UploadsController : ControllerBase
    {
       
        [HttpPost]
        [Route("image")]
        public IActionResult Index(IFormFile image)
        {
            if(image==null) return BadRequest("Vui lòng gửi file đính kèm");
            var path = "wwwroot/uploads";
            var fileName = Guid.NewGuid().ToString() + Path.GetFileName(image.FileName);
            var upload = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            image.CopyTo(new FileStream(upload, FileMode.Create));
            var rs = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            return Ok(rs);
        }
    }
}
