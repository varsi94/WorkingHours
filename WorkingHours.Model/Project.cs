using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.Repository;

namespace WorkingHours.Model
{
    public class Project : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public ICollection<UserProject> AssociatedMembers { get; set; }

        public bool IsClosed { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Project()
        {
            AssociatedMembers = new HashSet<UserProject>();
        }
    }
}
