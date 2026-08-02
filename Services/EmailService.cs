using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace DriveHubMongo.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendOtpEmail(string toEmail, string otp)
        {
            var fromEmail = _configuration["EmailSettings:FromEmail"];
            var appPassword = _configuration["EmailSettings:AppPassword"];

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail!);
            mail.To.Add(toEmail);
            mail.Subject = "DriveHub Password Reset OTP";

            mail.Body =
$@"Hello,

Your OTP for resetting your DriveHub password is:

{otp}

This OTP is valid for 5 minutes.

Do not share this OTP with anyone.

Regards,
DriveHub Team";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromEmail, appPassword);
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(mail);
        }
    }
}