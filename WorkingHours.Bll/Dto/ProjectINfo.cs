using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Bll.Dto
{
    public class ProjectInfo : ProjectHeader
    {
        public List<UserHeaderDto> Members { get; set; }

        public List<IssueHeader> Issues { get; set; }
    }
}
