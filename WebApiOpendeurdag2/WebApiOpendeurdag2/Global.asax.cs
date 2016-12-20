using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebApiOpendeurdag2.MessageHandlers;

namespace WebApiOpendeurdag2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Fix JSON serialization for entities with nested models
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            // Register key & authorization handler
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyHandler());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthHandler());

            // Add help pages
            AreaRegistration.RegisterAllAreas();
        }
    }
}
