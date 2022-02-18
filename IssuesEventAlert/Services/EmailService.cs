using IssuesEventAlert.Model;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IssuesEventAlert.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel emailModel);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task<bool> SendEmail(EmailModel emailModel)
        {
            var networkCredential = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);

            var client = new SmtpClient(_emailSettings.Host)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = networkCredential,
                Port = 587,
                EnableSsl = true
            };

            var mailAddress = new MailAddress(_emailSettings.Email, _emailSettings.DisplayName);

            var mailMessage = new MailMessage()
            {
                Subject = emailModel.Subject,
                SubjectEncoding = Encoding.UTF8,
                From = mailAddress,
                Body = emailModel.Message,
                BodyEncoding = Encoding.UTF8
            };

            foreach (var destinatary in emailModel.Destinataries)
            {
                mailMessage.To.Add(new MailAddress(destinatary));
            }

            client.Send(mailMessage);

            return true;
        }
    }
}
