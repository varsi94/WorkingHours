using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace WorkingHours.Desktop.Common
{
    public abstract class ValidatableViewModelBase : ViewModelBase, IDataErrorInfo
    {
        protected Dictionary<string, Func<object>> PropertiesToValidate { get; } =
            new Dictionary<string, Func<object>>();

        private string ValidateProperty(string columnName)
        {
            var validationResult = new List<ValidationResult>();
            if (Validator.TryValidateProperty(PropertiesToValidate[columnName].Invoke(),
                new ValidationContext(this) { MemberName = columnName }, validationResult))
            {
                return null;
            }

            return validationResult.First().ErrorMessage;
        }

        public virtual string this[string columnName]
        {
            get
            {
                string msg = ValidateProperty(columnName);
                IsValid = (msg == null) && ValidateProperties();
                OnValidation(columnName, msg);
                return msg;
            }
        }

        public virtual string Error => null;

        public ValidatableViewModelBase()
        {
            FillInProperties();
        }

        protected abstract void FillInProperties();

        private bool isValid = false;

        public bool IsValid
        {
            get { return isValid; }

            private set { Set(ref isValid, value); }
        }

        private bool ValidateProperties()
        {
            foreach (var prop in PropertiesToValidate)
            {
                if (ValidateProperty(prop.Key) != null)
                {
                    return false;
                }
            }
            return true;
        }

        protected virtual void OnValidation(string propertyName, string value)
        {
            
        }
    }
}
