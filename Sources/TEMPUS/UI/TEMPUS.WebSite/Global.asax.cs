using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TEMPUS.Infrastructure.ConrollerFactory;
using TEMPUS.WebSite.Helpers;
using TEMPUS.WebSite.Security;

namespace TEMPUS.WebSite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bootstrapper.ConfigureUnityContainer();
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
        }

        /// <summary>
        /// Authenticate request event handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                var principal = SitePrincipalFactory.CreatePrincipal(HttpContext.Current.User);
                HttpContext.Current.User = principal;
            }
        }
    }
}