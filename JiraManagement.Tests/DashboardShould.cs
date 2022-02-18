using JiraManagement.Bl.Dto;
using JiraManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace JiraManagement.Tests
{
    public class DashboardShould : BaseTestService
    {
        private readonly ITestOutputHelper _output;

        public DashboardShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task CreateDashboard()
        {
            var dashboard = new DashboardDto
            {
                Id = "2",
                Name = "Banreservas",
                Description = "Banco de Rep Dom",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            Assert.NotNull(addedDashboard);
        }

        [Fact]
        public async Task UpdateDashboard()
        {
            var dashboard = new DashboardDto
            {
                Id = "2",
                Name = "Banreservas",
                Description = "Banco de Rep Dom",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var update = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var updatedDashboard = await _dashboardService.Update(addedDashboard.Id, update);

            Assert.NotNull(updatedDashboard);
        }

        [Fact]
        public async Task Delete()
        {
            var dashboard = new DashboardDto
            {
                Id = "2",
                Name = "Banreservas",
                Description = "Banco de Rep Dom",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var isDeleted = await _dashboardService.Delete(addedDashboard.Id);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task GetDashboardById()
        {
            var dashboard = new DashboardDto
            {
                Name = "Tropigas",
                Description = "Empresa de Combustibles",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var result = await _dashboardService.GetById(addedDashboard.Id);

            Assert.NotNull(result);

        }

        [Fact]
        public async Task GetAll()
        {
            var dashboard1 = new DashboardDto
            {
                Id = "3",
                Name = "Tropigas",
                Description = "Empresa de Combustibles",
                IsDeleted = false
            };
            var dashboard2 = new DashboardDto
            {
                Id = "2",
                Name = "Banreservas",
                Description = "Banco de Rep Dom",
                IsDeleted = false
            };
            var dashboard3 = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };
            var dashboardList = new List<DashboardDto>()
            {
                dashboard1, dashboard2, dashboard3
            };

            var entityList = _mapper.Map<List<Dashboard>>(dashboardList);

            _context.AddRange(entityList);
            
            await _context.SaveChangesAsync();

            var allDashboards = await _dashboardService.Get().ToListAsync();

            Assert.True(allDashboards.Count >= 3);
        }
    }
}
