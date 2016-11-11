using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;
using WorkingHours.Shared.Dto;

namespace WorkingHours.Bll.AutoMapperConfig
{
    public class Configuration : Profile
    {
        public Configuration()
        {
            CreateMap<ApplicationUser, UserHeaderDto>();
            CreateMap<IPagedList<ApplicationUser>, PagedResult<UserHeaderDto>>()
                .ConvertUsing<PagedListConverter<ApplicationUser, UserHeaderDto>>();
            CreateMap<Project, ProjectHeader>();
            CreateMap<ApplicationUser, ProjectMemberDto>();
            CreateMap<Project, ProjectInfo>()
                .ForMember(p => p.Members, cfg => cfg.ResolveUsing<ProjectInfoResolver>());
            CreateMap<Issue, IssueHeader>();
            CreateMap<WorkTimeDto, WorkTime>();
        }
    }
}
