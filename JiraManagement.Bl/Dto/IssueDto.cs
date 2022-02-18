using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Bl.Dto
{
    public class IssueDto : BaseDto
    {
        public string UserId { get; set; }
        public string DashboardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
