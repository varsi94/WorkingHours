using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.Repository;

namespace WorkingHours.Model
{
    public class Issue : IDbEntity
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public DateTime? Deadline { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
