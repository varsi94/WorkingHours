using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Desktop.Interfaces;
using WorkingHours.Desktop.Interfaces.ViewModels;

namespace WorkingHours.Desktop.Model
{
    public class SearchEventArgs
    {
        public string Keyword { get; set; }

        public IPopulatable Populatable { get; set; }
    }
}
