using AutoMapper;
using JiraManagement.Bl.Dto;
using JiraManagement.Model.Context;
using JiraManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Services.Services
{
    public interface IDashboardService: IBaseService<Dashboard, DashboardDto, JiraContext>
    { }

    public class DashboardService : BaseService<Dashboard, DashboardDto, JiraContext>, IDashboardService
    {
        public DashboardService(JiraContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
