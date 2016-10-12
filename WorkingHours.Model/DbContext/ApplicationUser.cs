using System;
using Microsoft.AspNet.Identity.EntityFramework;
using WorkingHours.Model.Repository;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WorkingHours.Model.DbContext
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IDbEntity
    {
        public string FullName { get; set; }

        public ICollection<UserProject> Projects { get; set; }

        public ICollection<WorkTime> WorkItems { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ApplicationUser()
        {
            Projects = new HashSet<UserProject>();
            WorkItems = new HashSet<WorkTime>();
        }
    }
}