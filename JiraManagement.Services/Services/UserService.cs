using AutoMapper;
using JiraManagement.Bl.Dto;
using JiraManagement.Model.Context;
using JiraManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraManagement.Services.Services
{
    public interface IUserService : IBaseService<User, UserDto, JiraContext>
    {
        Task<List<UserDto>> GetUsers();
    }

    public class UserService : BaseService<User, UserDto, JiraContext>, IUserService
    {

        public UserService(JiraContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<UserDto> Create(UserDto dto)
        {
            var dashboard = await _context.Dashboards.Where(x => x.Id == dto.DashboardId).FirstOrDefaultAsync();

            if (dashboard is not null ) return await base.Create(dto);

            return null;
        }

        public override async Task<UserDto> GetById(string id)
        {
            var user = base.Get().FirstOrDefault(x => x.Id == id);

            if (user is null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = base.Get();

            var usersDto = _mapper.Map<List<UserDto>>(users);

            foreach(var user in usersDto)
            {
                var issues = await _context.Issues.Where(x => x.UserId == user.Id && x.IsDeleted == false).ToListAsync();

                if (issues.Count == 0) continue;

                else user.Issues.AddRange(_mapper.Map<List<IssueDto>>(issues));
            }
            return usersDto;
        }
    }
}
