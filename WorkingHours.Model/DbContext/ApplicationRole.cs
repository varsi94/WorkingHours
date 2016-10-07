using Microsoft.AspNet.Identity.EntityFramework;

namespace WorkingHours.Model.DbContext
{
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
    }
}