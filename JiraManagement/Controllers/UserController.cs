using JiraManagement.Bl.Dto;
using JiraManagement.Model.Context;
using JiraManagement.Model.Models;
using JiraManagement.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JiraManagement.Controllers
{
    public class UserController : BaseController<User, UserDto, JiraContext>
    {
        private readonly IUserService _userService;
        public UserController(IUserService service) : base(service)
        {
            _userService = service;
        }

        public override async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUsers());
        }
    }
}
