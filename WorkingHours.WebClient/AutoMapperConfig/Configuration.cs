using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WorkingHours.Client.Model;

namespace WorkingHours.WebClient.AutoMapperConfig
{
    public class Configuration : Profile
    {
        protected Configuration(string profileName) : base(profileName)
        {
        }

        public Configuration()
        {
            CreateMap<IPrincipal, LoginInfo>().ConvertUsing<PrincipalConverter>();
        }
    }
}