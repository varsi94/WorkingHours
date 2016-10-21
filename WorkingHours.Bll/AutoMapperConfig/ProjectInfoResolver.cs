using AutoMapper;
using System.Collections.Generic;
using WorkingHours.Model;
using System;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.AutoMapperConfig
{
    internal class ProjectInfoResolver : IValueResolver<Project, ProjectInfo, List<UserHeaderDto>>
    {
        public List<UserHeaderDto> Resolve(Project source, ProjectInfo destination, List<UserHeaderDto> destMember, ResolutionContext context)
        {
            destMember = new List<UserHeaderDto>();
            foreach (var associatedMember in source.AssociatedMembers)
            {
                var mappedMember = context.Mapper.Map<UserHeaderDto>(associatedMember.User);
                mappedMember.Role = associatedMember.Role.Name;
                destMember.Add(mappedMember);
            }
            return destMember;
        }
    }
}