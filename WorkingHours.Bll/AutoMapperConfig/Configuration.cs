using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingHours.Bll.Dto;
using WorkingHours.Model;
using WorkingHours.Model.DbContext;

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
        }
    }
}
