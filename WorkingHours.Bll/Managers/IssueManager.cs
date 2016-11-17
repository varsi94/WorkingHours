using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.Exceptions;
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

        public void UpdateIssue(int managerId, Issue issue)
        {
            try
            {
                var managerRole = UoW.Roles.GetRole(Roles.Manager);
                var issueFromDb = UoW.Issues.GetById(issue.Id, nameof(issue.Project) + "." + nameof(issue.Project.AssociatedMembers));
                if (issueFromDb == null)
                {
                    throw new NotFoundException("Issue not found!");
                }

                if (!issueFromDb.Project.AssociatedMembers.Any(x => x.UserId == managerId && x.RoleId == managerRole.Id))
                {
                    throw new UnauthorizedException("You are not a manager in this project!");
                }

                issueFromDb.RowVersion = issue.RowVersion;
                issueFromDb.Name = issue.Name;
                issueFromDb.Deadline = issue.Deadline;
                issueFromDb.Description = issue.Description;
                issueFromDb.IsClosed = issue.IsClosed;

                UoW.Issues.Update(issueFromDb);
                UoW.SaveChanges();
            }
            catch (ConcurrencyException)
            {
                throw new ConflictedException("Issue has been changed!");
            }
        }
    }
}
