using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WorkingHours.Web.IoC
{
    public class NinjectResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel Kernel { get; }

        public NinjectResolver(IKernel kernel) : base(kernel)
        {
            Kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(Kernel.BeginBlock());
        }
    }
}