using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TEMPUS.WebSite.Helpers
{
    public static class PageHelper
    {
        public static bool IsCurrentUrl(this UrlHelper helper, string actionName, string controllerName)
        {
            return helper.RequestContext.RouteData.Values["controller"].ToString() == controllerName &&
                helper.RequestContext.RouteData.Values["action"].ToString() == actionName;
        }

        public static HtmlString MainMenuItem(this HtmlHelper helper, string linkText, string actionName, string controllerName)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var tagBuilder = new TagBuilder("li");
            if (urlHelper.IsCurrentUrl(actionName, controllerName))
            {
                tagBuilder.AddCssClass("active");
            }
            tagBuilder.InnerHtml = helper.ActionLink(linkText, actionName, controllerName).ToHtmlString();

            return new HtmlString(tagBuilder.ToString());
        }
    }
}