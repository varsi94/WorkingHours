using System;
using System.Collections.Generic;
using System.Linq;
using WorkingHours.Bll.Exceptions;
using WorkingHours.Bll.Interfaces;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;
using WorkingHours.Model.UoW;
using WorkingHours.Model.Common;
using AutoMapper;
using System.Linq.Expressions;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.Managers
{
    public class UserManager : ManagerBase, IUserManager
    {
        public UserManager(IUnitOfWork UoW) : base(UoW)
        {
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            try
            {
                UoW.Users.ChangePassword(userId, oldPassword, newPassword);
                UoW.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("User id not found!");
            }
            catch (ArgumentException)
            {
                throw new UnauthorizedException("Current password is not correct!");
            }
        }

        public void CreateUser(ApplicationUser user, string password)
        {
            var result = UoW.Users.Add(user, password);
            if (!result.Succeeded)
            {
                throw new ArgumentException("Username is already taken!");
            }
            UoW.Users.AddToRole(user.Id, Roles.Employee);
            UoW.SaveChanges();
        }

        public PagedResult<UserHeaderDto> GetUsers(int pageIndex, int pageSize, string nameFilter)
        {
            var orderInfo = new OrderInfo<ApplicationUser>
            {
                Direction = SortDirection.Ascending,
                OrderBy = x => x.FullName
            };
            var result = UoW.Users.ListPaged((nameFilter == null) ? null : ((Expression<Func<ApplicationUser, bool>>)(x => x.FullName.Contains(nameFilter) || x.UserName.Contains(nameFilter))),
                pageIndex, pageSize, orderInfo, nameof(Roles));
            foreach (var applicationUser in result)
            {
                applicationUser.Role = UoW.Users.GetRoles(applicationUser).First();
            }
            return Mapper.Map<PagedResult<UserHeaderDto>>(result);
        }

        public void UpdateRoles(Dictionary<int, Roles> rolesToUpdate)
        {
            string managerRole = Roles.Manager.ToString();
            foreach (var item in rolesToUpdate)
            {
                int userId = item.Key;
                var user = UoW.Users.GetById(item.Key);
                if (item.Value == Roles.Employee && user.Role == Roles.Manager)
                {
                    var dummy = new Project();
                    IEnumerable<Project> projects =
                        UoW.Projects.List(
                            x => x.AssociatedMembers.Any(m => m.UserId == userId && m.Role.Name == managerRole), null,
                            nameof(dummy.AssociatedMembers), nameof(dummy.AssociatedMembers) + ".Role");
                    if (projects.Any())
                    {
                        throw new InvalidOperationException($"{user.FullName} has a project in which he/she is manager!");
                    }
                }

                var result = UoW.Users.AddToRole(item.Key, item.Value);
                if (!result.Succeeded)
                {
                    throw new InternalServerException($"User with {item.Key} could not be added to role!");
                }
            }
            UoW.SaveChanges();
        }
    }
}
