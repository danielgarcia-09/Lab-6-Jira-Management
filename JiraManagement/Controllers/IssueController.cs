using JiraManagement.Bl.Dto;
using JiraManagement.Model.Context;
using JiraManagement.Model.Models;
using JiraManagement.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JiraManagement.Controllers
{
    public class IssueController : BaseController<Issue, IssueDto, JiraContext>
    {
        private readonly IIssueService _issueService;
        public IssueController(IIssueService service) : base(service)
        {
            _issueService = service;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeOwnerIssue(string id, IssueDto dto)
        {
            var isUpdated = await _issueService.ChangeOwnerIssue(id, dto);

            if (isUpdated) return Ok();

            return NotFound();
        }
    }
}
