using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MakeKidsMoveApp.Models
{
   public class Registration
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserType { get; set; }
        public int ParentId { get; set; }
        public string UserTypeDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
