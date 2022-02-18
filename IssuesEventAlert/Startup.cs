using IssuesEventAlert.Model;
using IssuesEventAlert.Service;
using IssuesEventAlert.Services;
using JiraManagement.Model.Context;
using JiraManagement.Services.IoC;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(IssuesEventAlert.Startup))]
namespace IssuesEventAlert
{
    public class Startup : FunctionsStartup
    {
        IConfiguration Configuration;
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Configuration = builder.GetContext().Configuration;

            var cosmoEndpoint = Configuration["Cosmo-AccountEndpoint"];
            var cosmoKey = Configuration["Cosmo-AccountKey"];
            var cosmoDb = Configuration["Cosmo-DatabaseName"];
           
            builder.Services.AddDbContext<JiraContext>(op => op.UseCosmos(cosmoEndpoint, cosmoKey, cosmoDb));

            builder.Services.AddTransient<IIssueAlertService, IssueAlertService>();

            string email = Configuration["EmailSettings-Email"];
            string password = Configuration["EmailSettings-Password"];
            string host = Configuration["EmailSettings-Host"];
            string displayName = Configuration["EmailSettings-DisplayName"];

            var emailSettings = new EmailSettings(displayName, host, email, password);

            builder.Services.AddSingleton<IEmailService>(new EmailService(emailSettings));
            builder.Services.AddServiceRegistry();

        }
    }
}