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

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("roles")]
        public IEnumerable<string> Roles { get; set; }
    }

}
