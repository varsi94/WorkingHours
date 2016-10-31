using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WorkingHours.Desktop.Model;
using WorkingHours.Desktop.ViewModel;
using WorkingHours.Shared.Dto;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.Converters
{
    public class RoleChangedEventArgsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is UserViewModel && values[1] is Roles)
            {
                return new RoleChangedModel
                {
                    User =(UserViewModel) values[0],
                    NewRole = (Roles)values[1]
                };
            }
            else
            {
                throw new ArgumentException(nameof(values));
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
