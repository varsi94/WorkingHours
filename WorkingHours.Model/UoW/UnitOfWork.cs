using System;
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
    internal class UnitOfWork : IUnitOfWork
    {
        protected AppDbContext DbContext { get; }

        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext dbContext)
        {
            DbContext = dbContext;
            Users = new UserRepository(dbContext);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
