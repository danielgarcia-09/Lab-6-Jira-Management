using JiraManagement.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Services.IoC
{
    public static class ServiceRegistry
    {
        public static void AddServiceRegistry(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IIssueService, IssueService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
