using System;

namespace WorkingHours.Shared.Dto
{ 
    public class ProjectHeader
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public bool IsClosed { get; set; }
    }
}