using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Client.Model
{
    public class UserDataResult
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }

}
