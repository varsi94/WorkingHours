using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WorkingHours.Desktop.IoC
{
    public class ViewModelExtension : MarkupExtension
    {
        public Type ViewModelType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ServiceLocator.Get(ViewModelType);
        }
    }
}
