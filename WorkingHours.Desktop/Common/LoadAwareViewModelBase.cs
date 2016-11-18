using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.Common
{
    public abstract class LoadAwareViewModelBase : ViewModelBase, ILoadAware
    {
        public virtual Task OnHidden()
        {
            return Task.FromResult<object>(null);
        }

        public virtual Task OnShown()
        {
            return Task.FromResult<object>(null);
        }
    }
}
