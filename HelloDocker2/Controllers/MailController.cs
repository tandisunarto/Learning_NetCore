using Microsoft.AspNetCore.Mvc;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace HelloDocker2.Controllers
{
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        // the name "mail" comes from the docker-compose.yml service name
        const string MAIL_HOST = "mail";
        const int MAIL_PORT = 1025;

        [HttpGet]
        public string Get() {
            return "Test";
        }

        [HttpGet("sendtest")]
        public async void SendTestMail() {
             var message = new MimeMessage();
             message.From.Add(new MailboxAddress("HelloDocker2", "hellodocker2@demo.com"));
             message.To.Add(new MailboxAddress("", "tandi.sunarto@hotmail.com"));
             message.Subject = "Sample Email from HelloDocker 2";
             message.Body = new TextPart("plain") {
                 Text = "Greeting from HelloDocker2"
             };
             using (var mailClient = new SmtpClient()){
                 await mailClient.ConnectAsync(MAIL_HOST, MAIL_PORT, SecureSocketOptions.None);
                 await mailClient.SendAsync(message);
                 await mailClient.DisconnectAsync(true);
             }
        }
    }
}
