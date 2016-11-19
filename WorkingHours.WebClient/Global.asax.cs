using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WorkingHours.WebClient.AutoMapperConfig;
using WorkingHours.WebClient.Extensions;

namespace WorkingHours.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg => cfg.AddProfile(new Configuration()));

            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityExtensions.UserIdClaimType;
        }
    }
}
