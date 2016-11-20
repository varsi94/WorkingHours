using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public UserHeaderDto UserHeaderDto { get; }

        public string FullName
        {
            get { return UserHeaderDto.FullName; }
            set
            {
                UserHeaderDto.FullName = value;
                RaisePropertyChanged();
            }
        }

        public string Username
        {
            get { return UserHeaderDto.Username; }
            set
            {
                UserHeaderDto.Username = value;
                RaisePropertyChanged();
            }
        }

        public string Email
        {
            get { return UserHeaderDto.Email; }
            set
            {
                UserHeaderDto.Email = value;
                RaisePropertyChanged();
            }
        }

        public virtual Roles Role
        {
            get { return UserHeaderDto.RoleAsEnum; }
            set
            {
                UserHeaderDto.RoleAsEnum = value;
                RaisePropertyChanged();
            }
        }

        public int Id
        {
            get { return UserHeaderDto.Id; }
        }


        private bool isChanged;
        public bool IsChanged
        {
            get { return isChanged; }

            set { Set(ref isChanged, value); }
        }

        public UserViewModel(UserHeaderDto user)
        {
            UserHeaderDto = user;
        }
    }
}
