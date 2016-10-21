using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.UoW;

namespace WorkingHours.Bll.Managers
{
    public class IssueManager : ManagerBase, IIssueManager
    {
        public IssueManager(IUnitOfWork UoW) : base(UoW)
        {
        }

        public void AddIssueToProject(int projectId, Issue issue, int managerId)
        {
            var dummy = new Project();
            var project = UoW.Projects.GetById(projectId, nameof(dummy.AssociatedMembers), nameof(dummy.AssociatedMembers) + ".Role");
            if (project == null)
            {
                throw new NotFoundException("Project not found!");
            }

            if (!project.AssociatedMembers.Any(x => x.UserId == managerId && x.Role.Name == Roles.Manager.ToString()))
            {
                throw new UnauthorizedException("You are not a manager in this project!");
            }

            project.Issues.Add(issue);
            UoW.SaveChanges();
        }
    }
}
