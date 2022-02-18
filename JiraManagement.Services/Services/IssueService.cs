using AutoMapper;
using JiraManagement.Bl.Dto;
using JiraManagement.Model.Context;
using JiraManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JiraManagement.Services.Services
{
    public interface IIssueService : IBaseService<Issue, IssueDto, JiraContext>
    {
        Task<bool> ChangeOwnerIssue(string id, IssueDto dto);
    }

    public class IssueService : BaseService<Issue, IssueDto, JiraContext>, IIssueService
    {
        private readonly IUserService _userService;

        private readonly IDashboardService _dashboardService;

        private readonly IEmailService _emailService;
        public IssueService(IUserService userService, IDashboardService dashboardService,IEmailService emailService ,JiraContext context, IMapper mapper) : base(context, mapper)
        {
            _userService = userService;
            _dashboardService = dashboardService;
            _emailService = emailService;
        }

        public override async Task<IssueDto> Create(IssueDto dto)
        {
            var isValid = await IsValidIssue(dto);

            if (isValid) return await base.Create(dto);

            else return null;
        }

        public override async Task<IssueDto> Update(string id, IssueDto dto)
        {
            var isValid = await IsValidIssue(dto);

            if (isValid) return await base.Update(id, dto);

            else return null;
        }

        private async Task<bool> IsValidIssue(IssueDto dto)
        {
            if (dto.CreatedAt < DateTime.UtcNow) return false;

            var isValidDashboard = await _dashboardService.Get()
                .Where(x => x.Id == dto.DashboardId)
                .FirstOrDefaultAsync();

            if (isValidDashboard is null) return false;

            var isValidUser = await _userService.Get()
                .Where(x => x.Id == dto.UserId && x.DashboardId == dto.DashboardId)
                .FirstOrDefaultAsync();

            if (isValidDashboard is not null && isValidUser is not null) return true;

            return false;
        }

        public async Task<bool> ChangeOwnerIssue(string id, IssueDto dto)
        {
            var issueWithOldOwner = await GetById(id);

            if (issueWithOldOwner is null) return false;

            var oldOwner = await _userService.GetById(issueWithOldOwner.UserId);

            var newOwner = await _userService.GetById(dto.UserId);

            if(newOwner.DashboardId.Equals(issueWithOldOwner.DashboardId) )
            {
                issueWithOldOwner.UserId = dto.UserId;

                var issueWithNewOwner = await Update(id, issueWithOldOwner);

                if (issueWithNewOwner is null) return false;

                var emailSent = await _emailService.SendEmailAboutIssueChange(issueWithNewOwner, oldOwner);

                if(emailSent) return true;

                return false;
            }

            return false;
        }
    }
}
