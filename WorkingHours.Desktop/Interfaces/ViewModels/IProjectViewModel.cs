using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.Interfaces.ViewModels
{
    public interface IProjectViewModel : IViewModel
    {
        Roles RoleInProject { get; }
    }
}
