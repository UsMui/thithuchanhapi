using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nhomnetapi.Models;
using nhomnetapi.Services;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/mail")]
    
    public class MailController : ControllerBase
    {
        private readonly Services.IMailService _mail;

        public MailController(Services.IMailService mail)
        {
            _mail = mail;
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMailAsync(MailData mailData)
        {
            bool result = await _mail.SendAsync(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }
    }
}
