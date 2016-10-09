using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Client.IoC;

namespace WorkingHours.Desktop.IoC
{
    public static class ServiceLocator
    {
        private static IKernel Kernel { get; } = new StandardKernel(new DesktopModule());

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static object Get(Type type)
        {
            return Kernel.Get(type);
        }
    }
}
