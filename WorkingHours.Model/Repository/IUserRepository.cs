﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;

namespace WorkingHours.Model.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser Get(string userName, string password);

        void Add(ApplicationUser user, string password);
    }
}
