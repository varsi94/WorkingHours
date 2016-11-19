using AutoMapper;
using System.Security.Principal;
using WorkingHours.Client.Model;
using System;
using WorkingHours.WebClient.Extensions;

namespace WorkingHours.WebClient.AutoMapperConfig
{
    public class PrincipalConverter : ITypeConverter<IPrincipal, LoginInfo>
    {
        public LoginInfo Convert(IPrincipal source, LoginInfo destination, ResolutionContext context)
        {
            return new LoginInfo
            {
                UserName = source.GetUsername(),
                Email = source.GetEmail(),
                FullName = source.GetFullname(),
                Id = source.GetUserId(),
                IsLoggedIn = source.Identity.IsAuthenticated,
                Role = source.GetRole(),
                Token = source.GetToken()
            };
        }
    }
}