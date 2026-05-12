using EmailSending.Entities;
using EmailSending.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSending.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailSender;

        public EmailController(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public IActionResult Send([FromBody] EmailRequest request)
        {
            if (request == null || request.To == null || request.To.Count == 0)
            {
                return BadRequest("Email request must include at least one recipient.");
            }

            var message = new Message(request.To, request.Subject, request.Content);
            _emailSender.SendEmail(message);

            return Ok(new { Message = "Email sent successfully." });
        }

        public sealed record EmailRequest(List<string> To, string Subject, string Content);
    }
}
