using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject.Activation.Blocks;

namespace WorkingHours.Web.IoC
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot ResolutionRoot { get; }
        
        public NinjectDependencyScope(IResolutionRoot root)
        {
            ResolutionRoot = root;
        }

        public object GetService(Type serviceType)
        {
            return ResolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ResolutionRoot.GetAll(serviceType);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    (ResolutionRoot as IDisposable)?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}