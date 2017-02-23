using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");            
            
            routes.MapRoute(name: "Serial number", url: "serial/{letterCase}", defaults: new { controller = "Home", action = "Serial", letterCase = "upper" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "FormEdit",
               url: "{controller}/{action}/{answerForm}",
               defaults: new { controller = "Home", action = "Index", answerForm = typeof(List<Answer>) }
           );

        }
    }
}
