using System;
using Microsoft.AspNet.Identity.EntityFramework;
using WorkingHours.Model.Repository;
using System.ComponentModel.DataAnnotations;

namespace WorkingHours.Model.DbContext
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IDbEntity
    {
        public string FullName { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}