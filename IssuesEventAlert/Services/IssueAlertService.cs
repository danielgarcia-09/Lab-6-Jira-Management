using IssuesEventAlert.Model;
using IssuesEventAlert.Services;
using JiraManagement.Model.Context;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuesEventAlert.Service
{
    public interface IIssueAlertService
    {
        Task NotifyEvent(string Id, ILogger logger);
    }
    public class IssueAlertService : IIssueAlertService
    {
        private readonly JiraContext _context;

        private readonly IEmailService _emailService;

        public IssueAlertService(IEmailService emailService, JiraContext context)
        {
            _emailService = emailService;
            _context = context;
        }

        public async Task NotifyEvent(string Id, ILogger logger)
        {
            if (Id is null) return;

            var issue = await _context.Issues.FindAsync(Id);

            var user = await _context.Users.FindAsync(issue.UserId);

            var emailModel = new EmailModel();

            if (issue.CreatedAt > DateTime.UtcNow.AddMinutes(-2) && issue.IsDeleted is false)
            {
                logger.LogInformation("Creating Issue: " + Id);

                emailModel.Subject = $"Creating Issue: {Id}";

                emailModel.Message = $"The Issue with ID: {Id} was created at {issue.CreatedAt.ToLongTimeString()}";

            }else if (issue.IsDeleted)
            {
                logger.LogInformation("Deleting Issue " + Id);

                emailModel.Subject = $"Deleting Issue: {Id}";

                emailModel.Message = $"The Issue with ID: {Id} was deleted at {DateTime.UtcNow.ToLongTimeString()}";

            }
            else
            {
                logger.LogInformation("Updating Issue " + Id);

                emailModel.Subject = $"Updating Issue: {Id}";

                emailModel.Message = $"The Issue with ID: {Id} was updated at {DateTime.UtcNow.ToLongTimeString()}";
            }

            emailModel.Destinataries = new List<string>() { 
                user.Email
            };

            await _emailService.SendEmail(emailModel);
        }
    }
}
