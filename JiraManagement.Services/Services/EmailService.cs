using JiraManagement.Bl.Dto;
using JiraManagement.Core.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JiraManagement.Services.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel emailModel);

        Task<bool> SendEmailAboutIssueChange(IssueDto oldIssue, UserDto owner);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        private readonly IDashboardService _dashboardService;

        private readonly IUserService _userService;

        public EmailService(IDashboardService dashboardService, IUserService userService, IOptions<EmailConfig> emailConfig)
        {
            _dashboardService = dashboardService;
            _userService = userService;
            _emailConfig = emailConfig.Value;
        }

        public async Task<bool> SendEmail(EmailModel emailModel)
        {
            var networkCredential = new NetworkCredential(_emailConfig.Email, _emailConfig.Password);
            var client = new SmtpClient(_emailConfig.Host)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = networkCredential,
                Port = 587,
                EnableSsl = true
            };

            var mailAddress = new MailAddress(_emailConfig.Email, _emailConfig.DisplayName);

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

        public async Task<bool> SendEmailAboutIssueChange(IssueDto newIssue, UserDto lastOwner)
        {
            var dashboard = await _dashboardService.GetById(newIssue.DashboardId);

            var oldOwner = await _userService.GetById(lastOwner.Id);

            var newOwner = await _userService.GetById(newIssue.UserId);

            if (dashboard is null || oldOwner is null || newOwner is null) return false;

            var emailModel = new EmailModel
            {
                Subject = $"Issue change in Dashboard : {dashboard.Name}",
                Message = $"The issue with ID: {newIssue.Id} is being assigned from owner {oldOwner.Name} {oldOwner.LastName} to owner {newOwner.Name} {newOwner.LastName}",
                Destinataries = new List<string>
                {
                    oldOwner.Email,
                    newOwner.Email
                }
            };

            await SendEmail(emailModel);
            return true;
        }
    }
}
