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

            // Register key & authorization handler
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyHandler());
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthHandler());

            // Add help pages
            AreaRegistration.RegisterAllAreas();
        }
    }
}
