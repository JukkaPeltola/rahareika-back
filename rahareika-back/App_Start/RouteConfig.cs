using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rahareika_back
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SecondRoute",
                url: "{controller}/{action}/{startDate}/{endDate}",
                defaults: new { controller = "Home", action = "Index", startDate = "", endDate = "" }
            );

            routes.MapRoute(
            name: "ThirdRoute",
            url: "{controller}/{action}/{currency}/{startDate}/{endDate}",
            defaults: new { controller = "Home", action = "Index", currency = "", startDate = "", endDate = "" }
            );
        }
    }
}
