using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.Repository
{
    internal class RoleRepository : IRoleRepository
    {
        private AppDbContext DbContext { get; }

        public RoleRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationRole GetRole(Roles role)
        {
            var str = role.ToString();
            return DbContext.Roles.SingleOrDefault(x => x.Name == str);
        }
    }
}
