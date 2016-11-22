using System.Collections.Generic;

namespace WorkingHours.Shared.Dto
{
    public class ProjectInfo : ProjectHeader
    {
        public List<ProjectMemberDto> Members { get; set; }

        public List<IssueHeader> Issues { get; set; }
    }
}
