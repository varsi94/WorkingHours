using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface INewProjectViewModel : IViewModel
    {
        string Name { get; set; }

        DateTime? Deadline { get; set; }
    }
}
