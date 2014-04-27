using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TEMPUS.DB;
using TEMPUS.DB.Migrations;
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

            Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            Bootstrapper.ConfigureUnityContainer();
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
        }
    }
}
