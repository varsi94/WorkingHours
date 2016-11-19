using System;
using System.ComponentModel.DataAnnotations;

namespace WorkingHours.Shared.Dto
{ 
    public class ProjectHeader
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public bool IsClosed { get; set; }

        public bool IsActive { get; set; }
    }
}