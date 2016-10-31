using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.ViewModel;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.Model
{
    public class RoleChangedModel
    {
        public UserViewModel User { get; set; }

        public Roles NewRole { get; set; }
    }
}
