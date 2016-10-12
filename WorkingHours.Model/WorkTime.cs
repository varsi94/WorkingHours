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
    public class WorkTime : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Hours { get; set; }

        public Issue Issue { get; set; }

        public int IssueId { get; set; }

        public ApplicationUser Employee { get; set; }

        public int EmployeeId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
