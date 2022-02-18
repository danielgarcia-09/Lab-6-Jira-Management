using JiraManagement.Bl.Dto;
using JiraManagement.Model.Context;
using JiraManagement.Model.Models;
using JiraManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraManagement.Controllers
{
    public class DashboardController : BaseController<Dashboard,DashboardDto, JiraContext>
    {
        public DashboardController(IDashboardService service) : base(service)
        {
        }

    }
}
