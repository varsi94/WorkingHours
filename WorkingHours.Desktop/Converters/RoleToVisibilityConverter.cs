using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.Converters
{
    class RoleToVisibilityConverter : IValueConverter
    {
        public Visibility ManagerValue { get; set; }
        public Visibility EmployeeValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == Roles.Manager.ToString())
            {
                return ManagerValue;
            }
            else
            {
                return EmployeeValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
