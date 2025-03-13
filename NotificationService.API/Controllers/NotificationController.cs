using Microsoft.AspNetCore.Mvc;
using NotificationService.API.Services;

namespace NotificationService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly MailAppService _emailService;

        public NotificationController(MailAppService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SendRegistrationEmail")]
        public IActionResult SendRegistrationEmail([FromBody] RegistrationEmailRequest request)
        {
            _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
            return Ok();
        }
    }

    public class RegistrationEmailRequest
    {
        public IList<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}