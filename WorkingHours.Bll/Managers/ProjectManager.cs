using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Bll.Model;
using WorkingHours.Model;
using WorkingHours.Model.Common;
using WorkingHours.Model.Exceptions;
using WorkingHours.Model.UoW;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Managers
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        private readonly IReportingService reportingService;

        public ProjectManager(IUnitOfWork UoW, IReportingService reportingService) : base(UoW)
        {
            this.reportingService = reportingService;
        }

        public int Add(Project project, int managerId)
        {
            project.IsClosed = false;
            var association = new UserProject
            {
                UserId = managerId,
                Project = project,
                Role = UoW.Roles.GetRole(Roles.Manager),
                IsActive = true
            };
            project.AssociatedMembers.Add(association);
            UoW.Projects.Add(project);
            UoW.SaveChanges();
            return project.Id;
        }

        private void UpdateMembersForProject(int projectId, int managerId, Dictionary<int, Roles?> members, bool isAdd)
        {
            var dummy = new Project();
            var project = UoW.Projects.GetById(projectId, nameof(dummy.AssociatedMembers));
            if (project == null)
            {
                throw new NotFoundException("Project not found");
            }

            var managerRole = UoW.Roles.GetRole(Roles.Manager);
            if (!project.AssociatedMembers.Any(x => x.RoleId == managerRole.Id && x.UserId == managerId && x.IsActive))
            {
                throw new UnauthorizedException("You are not a manager in this project!");
            }

            foreach (var user in members)
            {
                int userId = user.Key;
                Roles? role = user.Value;
                var userProject = project.AssociatedMembers.SingleOrDefault(x => x.UserId == userId);
                var userToAdd = UoW.Users.GetById(userId);
                if (userToAdd == null)
                {
                    throw new NotFoundException("One user can not found!");
                }

                if (!UoW.Users.IsInRole(userId, Roles.Manager) && role == Roles.Manager)
                {
                    throw new InternalServerException("Employee can not be added as manager!");
                }

                if (userProject != null)
                {
                    userProject.IsActive = isAdd;
                    if (role.HasValue)
                    {
                        userProject.RoleId = UoW.Roles.GetRole(role.Value).Id;
                    }
                }
                else if (isAdd)
                {
                    if (!role.HasValue)
                    {
                        throw new InternalServerException("Role must have a value!");
                    }

                    userProject = new UserProject
                    {
                        IsActive = true,
                        UserId = userId,
                        ProjectId = projectId,
                        Role = UoW.Roles.GetRole(role.Value)
                    };
                    project.AssociatedMembers.Add(userProject);
                }
            }
            UoW.SaveChanges();
        }

        public void AddMembersToProject(int projectId, int managerId, Dictionary<int, Roles> users)
        {
            UpdateMembersForProject(projectId, managerId, users.ToDictionary(x => x.Key, x => (Roles?)x.Value), true);
        }

        public ProjectInfo GetProjectInfo(int projectId, int userId)
        {
            var dummy = new Project();
            var project = UoW.Projects.GetById(projectId, nameof(dummy.AssociatedMembers), nameof(dummy.Issues), nameof(dummy.AssociatedMembers) + ".User.Roles",
                nameof(dummy.AssociatedMembers) + ".Role");
            if (project == null)
            {
                throw new NotFoundException("Project not found!");
            }

            if (!project.AssociatedMembers.Any(m => m.UserId == userId))
            {
                throw new UnauthorizedException("User is not associated to this project!");
            }

            var managerRole = UoW.Roles.GetRole(Roles.Manager);
            foreach (var member in project.AssociatedMembers)
            {
                member.User.Role = (member.User.Roles.Any(r => r.RoleId == managerRole.Id)) ? Roles.Manager : Roles.Employee;
            }

            var result = Mapper.Map<ProjectInfo>(project);
            result.IsWriteable = result.Members.Any(x => x.Id == userId && x.IsActive);
            return result;
        }

        public List<ProjectHeader> List(int userId)
        {
            var dummy = new Project();
            var orderInfo = new OrderInfo<Project, string>
            {
                Direction = SortDirection.Ascending,
                OrderBy = (x) => x.Name
            };
            var projects = UoW.Projects.List(x => x.AssociatedMembers.Any(y => y.UserId == userId), orderInfo,
                nameof(dummy.AssociatedMembers));
            var actives = projects.ToDictionary(x => x.Id, x => x.AssociatedMembers.Single(m => m.UserId == userId).IsActive);
            var result = Mapper.Map<List<ProjectHeader>>(projects);
            foreach (var projectHeader in result)
            {
                projectHeader.IsWriteable = actives[projectHeader.Id];
            }
            return result;
        }

        public void RemoveMembersFromProject(int projectId, int managerId, List<int> userIds)
        {
            UpdateMembersForProject(projectId, managerId, userIds.ToDictionary(x => x, y => (Roles?)null), false);
        }

        public byte[] GetReport(int userId, int projectId, DateTime? startDate, DateTime? endDate)
        {
            var user = UoW.Users.GetById(userId);
            var dummy = new Project();
            var project =
                UoW.Projects.List<object>(x => x.Id == projectId, propsToInclude: new[] {nameof(dummy.AssociatedMembers)})
                    .SingleOrDefault();
            if (project == null)
            {
                throw new NotFoundException("Project not found!");
            }

            if (!project.AssociatedMembers.Any(x => x.UserId == userId))
            {
                throw new UnauthorizedException("You are not associated to this project!");
            }

            var items = UoW.WorkTimeLog.GetWorkTimesForProject(projectId, userId, startDate, endDate);
            var reportData = new ReportData
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Project = project,
                WorkTimeList = items
            };

            return reportingService.GetProjectWorkTimeReport(reportData);
        }

        public void UpdateProject(int userId, ProjectHeader projectHeader)
        {
            var projectInDb = UoW.Projects.GetById(projectHeader.Id, nameof(Project.AssociatedMembers) + ".Role");
            if (projectInDb == null)
            {
                throw new NotFoundException("Project not found!");
            }
            var managerStr = Roles.Manager.ToString();
            if (!projectInDb.AssociatedMembers.Any(m => m.UserId == userId && m.Role.Name == managerStr))
            {
                throw new UnauthorizedException("You are not a manager in this project!");
            }

            if (projectInDb.AssociatedMembers.Any(m => m.UserId == userId && !m.IsActive))
            {
                throw new UnauthorizedException("You are deactivated in this project!");
            }

            projectInDb.Name = projectHeader.Name;
            projectInDb.IsClosed = projectHeader.IsClosed;
            projectInDb.Deadline = projectHeader.Deadline;
            projectInDb.RowVersion = projectHeader.RowVersion;

            try
            {
                UoW.Projects.Update(projectInDb);
                UoW.SaveChanges();
            }
            catch (ConcurrencyException)
            {
                throw new ConflictedException("Somebody already updated this project!");
            }
        }
    }
}
