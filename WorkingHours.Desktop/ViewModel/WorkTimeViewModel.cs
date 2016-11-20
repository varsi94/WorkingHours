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
        private WorkTimeDto copy;
        private bool isEdit = false;

        public WorkTimeViewModel(WorkTimeDto workTime)
        {
            WorkTimeDto = workTime;
        }

        public void BeginEdit()
        {
            if (isEdit)
            {
                return;
            }

            copy = new WorkTimeDto
            {
                Date = WorkTimeDto.Date,
                Description = WorkTimeDto.Description,
                Name = WorkTimeDto.Name,
                RowVersion = WorkTimeDto.RowVersion,
                Hours = WorkTimeDto.Hours,
                Id = WorkTimeDto.Id
            };
            isEdit = true;
        }

        public void CancelEdit()
        {
            if (!isEdit)
            {
                return;
            }

            WorkTimeDto = copy;
            isEdit = false;
            foreach (var propertyInfo in GetType().GetProperties())
            {
                RaisePropertyChanged(propertyInfo.Name);
            }
        }

        public void EndEdit()
        {
            if (!isEdit)
            {
                return;
            }

            copy = null;
            isEdit = false;
        }

        [Required(ErrorMessage = "Name is required!")]
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

        [Range(0, 24, ErrorMessage = "Hours must be between 0 and 24 hours!")]
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
            PropertiesToValidate.Add(nameof(Hours), () => Hours);
        }

        public virtual bool IsReadonly => !WorkTimeDto.CanUpdate;
    }
}
