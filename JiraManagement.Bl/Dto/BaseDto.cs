using System;
using System.Collections.Generic;
using System.Text;

namespace JiraManagement.Bl.Dto
{
    public class BaseDto
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
