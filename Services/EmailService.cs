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

            // IMPORTANT
            mail.IsBodyHtml = true;

            mail.Body = $@"
<!DOCTYPE html>
<html>

<head>
<meta charset='UTF-8'>

<style>

body {{
    margin:0;
    padding:0;
    background:#f3f5f7;
    font-family:Arial,Helvetica,sans-serif;
}}

.wrapper{{
    width:100%;
    padding:40px 0;
}}

.container{{
    width:600px;
    margin:auto;
    background:#ffffff;
    border-radius:12px;
    overflow:hidden;
    box-shadow:0 3px 10px rgba(0,0,0,.15);
}}

.header{{
    background:#0d6efd;
    color:white;
    text-align:center;
    padding:30px;
}}

.header h1{{
    margin:0;
    font-size:38px;
}}

.header p{{
    margin-top:8px;
    font-size:18px;
}}

.content{{
    padding:40px;
    color:#444;
    line-height:30px;
}}

.content h2{{
    margin-top:0;
}}

.otp-box{{
    width:250px;
    margin:35px auto;
    background:#0d6efd;
    color:white;
    text-align:center;
    font-size:42px;
    font-weight:bold;
    letter-spacing:10px;
    padding:22px;
    border-radius:12px;
}}

.note{{
    background:#f8f9fa;
    border-left:5px solid #0d6efd;
    padding:15px;
    margin-top:30px;
}}

.footer{{
    text-align:center;
    padding:25px;
    color:#777;
    font-size:15px;
}}

</style>

</head>

<body>

<div class='wrapper'>

<div class='container'>

<div class='header'>
<h1>🚗 DriveHub</h1>
<p>Password Reset Verification</p>
</div>

<div class='content'>

<h2>Hello,</h2>

<p>
We received a request to reset your DriveHub account password.
Please use the verification code below to continue.
</p>

<div class='otp-box'>
{otp}
</div>

<div class='note'>
⏰ <b>This OTP is valid for 5 minutes.</b><br><br>
🔒 Never share this OTP with anyone.
</div>

<p style='margin-top:35px;'>
If you didn't request a password reset, you can safely ignore this email.
</p>

</div>

<div class='footer'>

Regards,<br>
<b>DriveHub Team</b>

</div>

</div>

</div>

</body>

</html>";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromEmail, appPassword);
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(mail);
        }
    }
}