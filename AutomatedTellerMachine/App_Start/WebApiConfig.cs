using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AutomatedTellerMachine
{
    public static class WebApiConfig
    {
        

        public static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
             name: "GetStatusLoginCredentials",
             routeTemplate: "api/{controller}/{email}/{password}"
);
        }
    }
}
