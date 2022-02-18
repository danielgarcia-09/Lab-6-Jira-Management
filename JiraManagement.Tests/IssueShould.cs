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
    public class IssueShould : BaseTestService
    {
        private readonly ITestOutputHelper _output;

        public IssueShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task AddIssue()
        {
            var dashboard = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "1",
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                IsDeleted = false
            };
            var addedUser = await _userService.Create(user);

            var issue = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Deploy de Azure",
                Description = "Hacer un deployment de un service bus trigger",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var addedIssue = _context.Issues.Add(issue).Entity;

            await _context.SaveChangesAsync();

            Assert.Same(issue, addedIssue);
            _output.WriteLine($"old issue id: {issue.Id} and added issue id: {addedIssue.Id}");
        }

        [Fact]
        public async Task GetIssueById()
        {
            var dashboard = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "1",
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                IsDeleted = false
            };
            var addedUser = await _userService.Create(user);

            var issue = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Deploy de Azure",
                Description = "Hacer un deployment de un service bus trigger",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var addedIssue = _context.Issues.Add(issue).Entity;

            await _context.SaveChangesAsync();

            var searchedIssue = await _context.Issues.FindAsync(addedIssue.Id);

            Assert.Same(addedIssue, searchedIssue);
        }

        [Fact]
        public async Task GetAllIssues()
        {
            var dashboard = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "1",
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                IsDeleted = false
            };
            var addedUser = await _userService.Create(user);

            var issue1 = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Deploy de Azure",
                Description = "Hacer un deployment de un service bus trigger",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };


            var issue2 = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Formulario en React",
                Description = "Formulario con campos arrastrables en React",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var issue3 = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Jsonwebtoken",
                Description = "Implementar JWT en Node.js",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var issueList = new List<Issue>()
            {
                issue1, issue2, issue3
            };

            _context.Issues.AddRange(issueList);

            await _context.SaveChangesAsync();

            var allIssues = await _context.Issues.ToListAsync();

            Assert.Contains(issue2, allIssues);
        }

        [Fact]
        public async Task UpdateIssue()
        {
            var dashboard = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "1",
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                IsDeleted = false
            };
            var addedUser = await _userService.Create(user);

            var issue = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Deploy de Azure",
                Description = "Hacer un deployment de un service bus trigger",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var addedIssue = _context.Issues.Add(issue).Entity;

            await _context.SaveChangesAsync();

            var newIssue = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Jsonwebtoken",
                Description = "Implementar JWT en Node.js",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            addedIssue.Title = newIssue.Title;
            addedIssue.Description = newIssue.Description;

            var updated = _context.Issues.Update(addedIssue).Entity;

            await _context.SaveChangesAsync();

            Assert.NotNull(updated);

        }

        [Fact]
        public async Task Delete()
        {
            var dashboard = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "1",
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                IsDeleted = false
            };

            var addedUser = await _userService.Create(user);

            var issue = new Issue
            {
                DashboardId = addedDashboard.Id,
                UserId = addedUser.Id,
                Title = "Deploy de Azure",
                Description = "Hacer un deployment de un service bus trigger",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            var addedIssue = _context.Issues.Add(issue).Entity;

            await _context.SaveChangesAsync();

            var foundIssue = await _context.Issues.FindAsync(addedIssue.Id);

            var deleted = _context.Issues.Remove(foundIssue).Entity;

            await _context.SaveChangesAsync();

            Assert.NotNull(deleted);
        }
    }
}
