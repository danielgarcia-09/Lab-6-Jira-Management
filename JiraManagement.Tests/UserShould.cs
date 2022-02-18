using JiraManagement.Bl.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace JiraManagement.Tests
{
    public class UserShould : BaseTestService
    {
        private readonly ITestOutputHelper _output;

        public UserShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task AddUser()
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
            var result = await _userService.Create(user);
            Assert.NotNull(result);

            _output.WriteLine(result.Name);
        }

        [Fact]
        public async Task GetUsers()
        {
            var dashboard = new DashboardDto
            {
                Id = "1",
                Name = "Solvex",
                Description = "El final",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user1 = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "1",
                Name = "Daniel",
                LastName = "Garcia",
                Email = "dgarcia@gmail.com",
                IsDeleted = false
            };
            var user2 = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "2",
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                IsDeleted = false
            };
            var user3 = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "3",
                Name = "Pedro",
                LastName = "Gonzaga",
                Email = "pgonzaga@gmail.com",
                IsDeleted = false
            };
            await _userService.Create(user1);
            await _userService.Create(user2);
            await _userService.Create(user3);

            var allUsers = await _userService.Get().ToListAsync();

            Assert.NotNull(allUsers);
            Assert.True(allUsers.Count >= 2);
        }

        [Fact]
        public async Task GetUserById()
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
                Id = "2",
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                IsDeleted = false
            };

            var addedUser = await _userService.Create(user);

            var result = await _userService.GetById(addedUser.Id);

            Assert.NotNull(result);
            _output.WriteLine(result.Name);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var dashboard = new DashboardDto
            {
                Id = "2",
                Name = "Banreservas",
                Description = "Banco de Rep Dom",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "3",
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                IsDeleted = false
            };

            var addedUser = await _userService.Create(user);

            var userUpdate = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "2",
                Name = "Pedro",
                LastName = "Gonzaga",
                Email = "pgonzaga@gmail.com",
                IsDeleted = false
            };

            var updatedUser = await _userService.Update(addedUser.Id, userUpdate);

            Assert.NotEqual(addedUser, updatedUser);

        }

        [Fact]
        public async Task DeleteUser()
        {
            var dashboard = new DashboardDto
            {
                Id = "2",
                Name = "Banreservas",
                Description = "Banco de Rep Dom",
                IsDeleted = false
            };

            var addedDashboard = await _dashboardService.Create(dashboard);

            var user = new UserDto
            {
                DashboardId = addedDashboard.Id,
                Id = "4",
                Name = "Hector",
                LastName = "Gonzo",
                Email = "hgonzo@gmail.com",
                IsDeleted = false
            };

            var addedUser = await _userService.Create(user);

            var isDeleted = await _userService.Delete(addedUser.Id);

            Assert.True(isDeleted);
        }

    }
}
