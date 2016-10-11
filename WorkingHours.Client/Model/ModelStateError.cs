using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Client.Model
{

    public class ModelState
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("modelState")]
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
