using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPUS.BaseDomain.Messages;
using TEMPUS.WebSite.Models.Base;

namespace TEMPUS.WebSite.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Returnrs model's editor template view. Editor template must be called by model Type name
        /// </summary>
        /// <param name="model">the editor model</param>
        /// <param name="prefix">Prefix which will be added to to editor inputs if not null</param>
        protected virtual PartialViewResult EditorFor(object model, string prefix = null)
        {
            if (model == null)
            {
                throw new ArgumentException("model");
            }

            var result = PartialView(
                    String.Format(CultureInfo.InvariantCulture, "{0}Views/Shared/EditorTemplates/{1}.cshtml", GetAreaPath(), model.GetType().Name),
                    model);

            if (!String.IsNullOrWhiteSpace(prefix))
            {
                var viewData = new ViewDataDictionary(result.ViewData)
                {
                    TemplateInfo = new TemplateInfo
                    {
                        HtmlFieldPrefix = prefix
                    }
                };

                result.ViewData = viewData;
            }

            return result;
        }

        /// <summary>
        /// Returnrs model's display template view. Display template must be called by model Type name
        /// </summary>
        protected virtual PartialViewResult DisplayFor(object model)
        {
            if (model == null)
            {
                throw new ArgumentException("model");
            }

            var result = PartialView(
                    String.Format(CultureInfo.InvariantCulture, "{0}Views/Shared/DisplayTemplates/{1}.cshtml", GetAreaPath(), model.GetType().Name),
                    model);

            return result;
        }

        protected virtual string GetAreaPath()
        {
            return "~/";
        }

        public ActionResult TopNavigation()
        {
            var model = new TopNavigationViewModel("TEMPUS", false);
            return DisplayFor(model);
        }
    }
}