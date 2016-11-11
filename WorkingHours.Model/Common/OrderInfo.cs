using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.Common
{
    public class OrderInfo<T, TKey>
    {
        public Expression<Func<T, TKey>> OrderBy { get; set; }

        public SortDirection Direction { get; set; }
    }
}
