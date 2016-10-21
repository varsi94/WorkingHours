using System.Collections.Generic;

namespace WorkingHours.Shared.Dto
{
    public class ProjectInfo : ProjectHeader
    {
        public List<UserHeaderDto> Members { get; set; }

        public List<IssueHeader> Issues { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
