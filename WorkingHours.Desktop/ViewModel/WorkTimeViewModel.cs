using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Common;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.ViewModel
{
    public class WorkTimeViewModel : ValidatableViewModelBase, IEditableObject
    {
        private static readonly CultureInfo enUS = new CultureInfo("en-US");

        public WorkTimeDto WorkTimeDto { get; protected set; }

        public WorkTimeViewModel(WorkTimeDto workTime)
        {
            this.WorkTimeDto = workTime;
        }

        public void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void EndEdit()
        {
            throw new NotImplementedException();
        }

        [Required]
        public string Name
        {
            get { return WorkTimeDto.Name; }
            set
            {
                WorkTimeDto.Name = value;
                RaisePropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return WorkTimeDto.Date; }
            set
            {
                WorkTimeDto.Date = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(DateStr));
            }
        }

        public string DateStr => Date.ToString(enUS.DateTimeFormat.LongDatePattern, enUS);

        public double Hours
        {
            get { return WorkTimeDto.Hours; }
            set
            {
                WorkTimeDto.Hours = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return WorkTimeDto.Description; }
            set
            {
                WorkTimeDto.Description = value;
                RaisePropertyChanged();
            }
        }
        
        protected override void FillInProperties()
        {
            PropertiesToValidate.Add(nameof(Name), () => Name);
        }

        public bool IsReadonly
        {
            get { return (DateTime.Now - Date).TotalDays >= 8; }
        }
    }
}
