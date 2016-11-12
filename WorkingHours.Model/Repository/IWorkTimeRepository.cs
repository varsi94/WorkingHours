using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.Repository
{
    public interface IWorkTimeRepository : IRepository<WorkTime>
    {
        List<WorkTime> GetWorkTimesForProject(int projectId, int userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
