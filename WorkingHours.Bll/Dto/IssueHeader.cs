using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Dto
{
    public class IssueHeader
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsClosed { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
