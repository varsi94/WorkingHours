using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.Repository
{
    public interface IDbEntity
    {
        int Id { get; set; }

        byte[] RowVersion { get; set; }
    }
}
