using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.ViewModel
{
    public class NewWorkTimeViewModel : WorkTimeViewModel
    {
        public NewWorkTimeViewModel(WorkTimeDto workTime) : base(workTime)
        {
        }

        public override bool IsReadonly => false;
    }
}
