using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace RestApplication.Infrastructure
{
    public class EmailSender
    {
        private string verEdnp = "https://localhost:7223/api/auth/verifyUser?token=";
        private string chgPw = "https://localhost:7223/api/auth/resetPassword?token=";
        private readonly EmailCredentials emailCredentials;


        public EmailSender(IOptions<EmailCredentials> emailCredentials)
        {
            this.emailCredentials = emailCredentials.Value;
        }


        public async Task SendVerificationEmail(string token, string toEmail)
        {
            var emailBody = "<html><body><a href=\"" + verEdnp + token
                            + "\">Verify by clicking here</a></body></html>";
            var subject = "StefanBeam Account Confirmation";
            SendEmail(toEmail, emailBody, subject);   
        }


        public async Task SendResetPasswordEmail(string token, string toEmail)
        {
            var emailBody = "<html><body><a href=\"" + chgPw + token
                            + "\">Confirm password change</a></body></html>";
            var subject = "StefanBeam Password Reset Request";
            SendEmail(toEmail, emailBody, subject);
        }


        private void SendEmail(string toEmail, string emailBody, string subject)
        {
            string fromEmail = emailCredentials.User;
            string fromPassword = emailCredentials.Password;

            MailMessage message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            // Might throw an error, handle it in the endpoint
            smtpClient.Send(message);
        }
    }
}
