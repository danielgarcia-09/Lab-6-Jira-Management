
using System;

namespace JiraManagement.Model.Models
{
    public class Issue : BaseEntity
    {
        public string UserId { get; set; }
        public string DashboardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
