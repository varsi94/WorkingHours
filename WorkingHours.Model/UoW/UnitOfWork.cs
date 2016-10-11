﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.Exceptions;
using WorkingHours.Model.Repository;

namespace WorkingHours.Model.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected AppDbContext DbContext { get; }

        public IUserRepository Users { get; }

        public IRepository<Project> Projects { get; }

        public IRepository<Issue> Issues { get; }

        public IRepository<WorkItem> WorkItems { get; }

        public UnitOfWork(AppDbContext dbContext)
        {
            DbContext = dbContext;
            Users = new UserRepository(dbContext);
            Projects = new GenericRepository<Project>(dbContext);
            Issues = new GenericRepository<Issue>(dbContext);
            WorkItems = new GenericRepository<WorkItem>(dbContext);
        }

        public void SaveChanges()
        {
            try
            {
                DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConcurrencyException();
            }
        }
    }
}
