using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkingHours.Bll.Dto
{ 
    public class ProjectHeader
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime? Deadline { get; set; }
    }
}