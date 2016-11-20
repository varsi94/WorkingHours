using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Common;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Desktop.ViewModel
{
    public class IssueViewModel : ValidatableViewModelBase, IEditableObject
    {
        private bool isEdit = false;
        private IssueHeader copy;
        private IssueHeader issue;

        [Required(ErrorMessage = "Name field is required!")]
        public string Name
        {
            get { return issue.Name; }
            set
            {
                issue.Name = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return issue.Description; }
            set
            {
                issue.Description = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? Deadline
        {
            get { return issue.Deadline; }
            set
            {
                issue.Deadline = value;
                RaisePropertyChanged();
            }
        }

        public bool IsClosed
        {
            get { return issue.IsClosed; }
            set
            {
                issue.IsClosed = value;
                RaisePropertyChanged();
            }
        }

        public int Id => issue.Id;

        public IssueViewModel(IssueHeader issue)
        {
            this.issue = issue;
        }

        public IssueViewModel()
        {
            this.issue = new IssueHeader();
        }

        public IssueHeader GetIssueHeader()
        {
            return issue;
        }

        public void BeginEdit()
        {
            if (isEdit)
            {
                return;
            }

            copy = new IssueHeader
            {
                Name = Name,
                Deadline = Deadline,
                Description = Description,
                IsClosed = IsClosed,
                Id = issue.Id,
                RowVersion = issue.RowVersion
            };
            isEdit = true;
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

        public void CancelEdit()
        {
            if (!isEdit)
            {
                return;
            }

            issue = copy;
            copy = null;
            isEdit = false;
            RaiseAllPropertiesChanged();
        }

        private void RaiseAllPropertiesChanged()
        {
            RaisePropertyChanged(() => Name);
            RaisePropertyChanged(() => Deadline);
            RaisePropertyChanged(() => Description);
            RaisePropertyChanged(() => IsClosed);
        }

        protected override void FillInProperties()
        {
            PropertiesToValidate.Add(nameof(Name), () => Name);
        }
    }
}
