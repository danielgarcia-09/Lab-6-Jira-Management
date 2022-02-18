using AutoMapper;
using JiraManagement.Bl.Mapping;
using JiraManagement.Model.Context;
using JiraManagement.Services.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace JiraManagement.Tests
{
    public class BaseTestService : IDisposable
    {
        private DbContextOptions<JiraContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<JiraContext>()
                .UseInMemoryDatabase(databaseName: "Jira")
                .Options;
        }

        protected readonly JiraContext _context;

        protected readonly IMapper _mapper;

        protected readonly IDashboardService _dashboardService;

        protected readonly IUserService _userService;

       public BaseTestService()
        {
            _context = new JiraContext(CreateOptions());

            _mapper = new MapperConfiguration(x => x.AddProfile<JiraProfile>())
                .CreateMapper();

            _dashboardService = new DashboardService(_context, _mapper);

            _userService = new UserService(_context, _mapper);
        }
        public void Dispose()
        {

        }
    }
}
