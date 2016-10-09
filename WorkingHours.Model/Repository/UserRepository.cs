using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.Repository
{
    internal class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public ApplicationUser Get(string userName, string password)
        {
            return DbContext.UserManager.Find(userName, password);
        }

        public override void Add(ApplicationUser obj)
        {
            throw new InvalidOperationException();
        }

        public override void Remove(ApplicationUser obj)
        {
            DbContext.UserManager.Delete(obj);
        }

        public override void Update(ApplicationUser obj)
        {
            DbContext.UserManager.Update(obj);
        }

        public IdentityResult Add(ApplicationUser user, string password)
        {
            return DbContext.UserManager.Create(user, password);
        }

        public IList<string> GetRoles(ApplicationUser user)
        {
            return DbContext.UserManager.GetRoles(user.Id);
        }

        public IdentityResult AddToRole(ApplicationUser user, Roles role)
        {
            return DbContext.UserManager.AddToRole(user.Id, role.ToString());
        }
    }
}
