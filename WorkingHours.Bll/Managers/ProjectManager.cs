using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.Common;
using WorkingHours.Model.UoW;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Managers
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        public ProjectManager(IUnitOfWork UoW) : base(UoW)
        {
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

        public void AddMembersToProject(int projectId, int managerId, Dictionary<int, Roles> users)
        {
            var dummy = new Project();
            var project = UoW.Projects.GetById(projectId, nameof(dummy.AssociatedMembers));
            if (project == null)
            {
                throw new NotFoundException("Project not found");
            }

            var managerRole = UoW.Roles.GetRole(Roles.Manager);
            if (!project.AssociatedMembers.Any(x => x.RoleId == managerRole.Id && x.UserId == managerId))
            {
                throw new UnauthorizedException("You are not a manager in this project!");
            }

            foreach (var user in users)
            {
                int userId = user.Key;
                Roles role = user.Value;
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
                    userProject.IsActive = true;
                    userProject.RoleId = UoW.Roles.GetRole(role).Id;
                }
                else
                {
                    userProject = new UserProject
                    {
                        IsActive = true,
                        UserId = userId,
                        RoleId = UoW.Roles.GetRole(role).Id,
                        ProjectId = projectId
                    };
                    project.AssociatedMembers.Add(userProject);
                }
            }
            UoW.SaveChanges();
        }

        public ProjectInfo GetProjectInfo(int projectId, int userId)
        {
            var dummy = new Project();
            var project = UoW.Projects.GetById(projectId, nameof(dummy.AssociatedMembers), nameof(dummy.Issues), nameof(dummy.AssociatedMembers) + ".User",
                nameof(dummy.AssociatedMembers) + ".Role");
            if (project == null)
            {
                throw new NotFoundException("Project not found!");
            }

            if (!project.AssociatedMembers.Any(m => m.UserId == userId))
            {
                throw new UnauthorizedException("User is not associated to this project!");
            }
            return Mapper.Map<ProjectInfo>(project);
        }

        public List<ProjectHeader> List(int userId)
        {
            var dummy = new Project();
            var orderInfo = new OrderInfo<Project>
            {
                Direction = SortDirection.Ascending,
                OrderBy = (x) => x.Name
            };
            var projects = UoW.Projects.List(x => x.AssociatedMembers.Any(y => y.UserId == userId), orderInfo,
                nameof(dummy.AssociatedMembers));
            return Mapper.Map<List<ProjectHeader>>(projects);
        }

        public void RemoveUsersFromProject(int projectId, int managerId, List<int> userIds)
        {
            throw new NotImplementedException();
        }
    }
}
