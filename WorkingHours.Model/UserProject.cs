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
    public class UserProject : IDbEntity
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public int UserId { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        public ApplicationRole Role { get; set; }

        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
