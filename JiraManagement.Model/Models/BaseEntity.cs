using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JiraManagement.Model.Models
{
    public interface IBaseEntity
    {
        public string Id { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class BaseEntity : IBaseEntity
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; set; }
    }
}
