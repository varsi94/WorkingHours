using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Dto;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.Common;
using WorkingHours.Model.UoW;

namespace WorkingHours.Bll.Managers
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        public ProjectManager(IUnitOfWork UoW) : base(UoW)
        {
        }

        public void Add(Project project, int managerId)
        {
            project.IsClosed = false;
            var association = new UserProject
            {
                UserId = managerId,
                Project = project,
                Role = UoW.Roles.GetRole(Roles.Manager)
            };
            project.AssociatedMembers.Add(association);
            UoW.Projects.Add(project);
            UoW.SaveChanges();
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
    }
}
