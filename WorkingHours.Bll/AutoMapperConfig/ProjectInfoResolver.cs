using AutoMapper;
using System.Collections.Generic;
using WorkingHours.Model;
using System;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.AutoMapperConfig
{
    internal class ProjectInfoResolver : IValueResolver<Project, ProjectInfo, List<ProjectMemberDto>>
    {
        public List<ProjectMemberDto> Resolve(Project source, ProjectInfo destination, List<ProjectMemberDto> destMember, ResolutionContext context)
        {
            destMember = new List<ProjectMemberDto>();
            foreach (var associatedMember in source.AssociatedMembers)
            {
                var mappedMember = context.Mapper.Map<ProjectMemberDto>(associatedMember.User);
                mappedMember.RoleInProject = associatedMember.Role.Name;
                mappedMember.Role = associatedMember.User.Role.ToString();
                mappedMember.IsActive = associatedMember.IsActive;
                destMember.Add(mappedMember);
            }
            return destMember;
        }
    }
}