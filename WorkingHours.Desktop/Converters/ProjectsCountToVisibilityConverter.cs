using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WorkingHours.Desktop.Converters
{
    class ProjectsCountToVisibilityConverter : IValueConverter
    {
        public Visibility VisibleValue { get; set; } 
        public Visibility CollapsedValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool res = false;
            res = (int)value == 0;

            if ((string)parameter == "Text")
            {
                return (res) ?  VisibleValue: CollapsedValue;
            }
            else
            {
                return (res) ? CollapsedValue: VisibleValue;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
