using Microsoft.Practices.Unity;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TEMPUS.Infrastructure.Unity;

namespace TEMPUS.Infrastructure.ConrollerFactory
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private static UnityControllerFactory _instance;

        public static UnityControllerFactory Instance
        {
            get { return _instance ?? (_instance = new UnityControllerFactory()); }
        }

        public static void SetInstance(UnityControllerFactory factory)
        {
            _instance = factory;
        }

        public ControllerBase GetErrorController()
        {
            return Container.Get<IController>("ErrorController") as ControllerBase;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return GetErrorController();
            }

            if (!typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(
                    String.Format("Type requested is not a controller: {0}", controllerType.Name), "controllerType");
            }

            try
            {
                return Container.Current.Resolve(controllerType) as IController;
            }
            catch (HttpException ex)
            {
                if (ex.GetHttpCode() != (int)HttpStatusCode.NotFound)
                {
                    throw;
                }

                return GetErrorController();
            }
        }
    }
}