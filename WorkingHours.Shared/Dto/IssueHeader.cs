using System;

namespace WorkingHours.Shared.Dto
{
    public class IssueHeader
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsClosed { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
