using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Client.Model
{
    public class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
