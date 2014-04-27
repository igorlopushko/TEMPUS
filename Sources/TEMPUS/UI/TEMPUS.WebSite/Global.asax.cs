using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TEMPUS.Infrastructure.ConrollerFactory;
using TEMPUS.WebSite.Helpers;

namespace TEMPUS.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
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
    }
}
