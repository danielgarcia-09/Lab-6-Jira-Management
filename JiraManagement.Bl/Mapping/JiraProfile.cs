using AutoMapper;
using JiraManagement.Bl.Dto;
using JiraManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Bl.Mapping
{
    public class JiraProfile : Profile
    {
        public JiraProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<IssueDto, Issue>().ReverseMap();

            CreateMap<IssueDto, Issue>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<DashboardDto, Dashboard>().ReverseMap();

            CreateMap<DashboardDto, Dashboard>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
