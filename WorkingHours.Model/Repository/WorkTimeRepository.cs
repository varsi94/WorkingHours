using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model.DbContext;
using System.Data.Entity;
using System.Linq.Expressions;

namespace WorkingHours.Model.Repository
{
    internal class WorkTimeRepository : GenericRepository<WorkTime>, IWorkTimeRepository
    {
        public WorkTimeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public List<WorkTime> GetWorkTimesForProject(int projectId, int userId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?))
        {
            Expression<Func<WorkTime, bool>> expr = null;
            if (startDate == null && endDate != null)
            {
                expr = x => x.Date <= endDate;
            }
            else if (startDate != null && endDate == null)
            {
                expr = x => x.Date >= startDate;
            }
            else if (startDate != null && endDate != null)
            {
                expr = x => x.Date >= startDate && x.Date <= endDate;
            }

            var query =
                DbContext.WorkTimeLog.Include(x => x.Issue.Project)
                    .Where(x => x.Issue.ProjectId == projectId && x.EmployeeId == userId);

            if (expr != null)
            {
                query = query.Where(expr);
            }
            return query.ToList();
        }
    }
}
