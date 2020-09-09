using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace MakeKidsMoveApp.Models
{
   public class Login
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
