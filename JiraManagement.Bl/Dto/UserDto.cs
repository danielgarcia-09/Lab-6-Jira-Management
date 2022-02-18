using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Bl.Dto
{
    public class UserDto : BaseDto
    {
        public string DashboardId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<IssueDto> Issues { get; set; } = new List<IssueDto>();
    }
}
