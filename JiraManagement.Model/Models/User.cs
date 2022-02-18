using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Model.Models
{
    public class User : BaseEntity
    {
        public string DashboardId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
    }
}
