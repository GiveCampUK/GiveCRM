using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GiveCRM.Web.Views
{
    public static class MenuHelper
    {
        public static bool IsCurrentPage(
            this HtmlHelper htmlHelper,
            string controllerName,
            string actionName = "")
        {
            var routeData = htmlHelper.ViewContext.RouteData;
            string currentAction = routeData.GetRequiredString("action");
            string currentController = routeData.GetRequiredString("controller");

            if (string.IsNullOrEmpty(actionName))
            {
                actionName = currentAction;
            }

            return (controllerName == currentController && actionName == currentAction);
        }

        public static MvcHtmlString NavigationClassBuilder(
            this HtmlHelper htmlHelper,
            string controllerName,
            string actionName = "",
            string additionalClasses = "")
        {
            List<string> classList = SplitString(additionalClasses);

            if (IsCurrentPage(htmlHelper, controllerName, actionName))
            {
                classList.Add("active");
            }

            if (classList.Count == 0)
            {
                return new MvcHtmlString("");
            }

            return new MvcHtmlString("class=\"" + JoinString(classList) + "\"");
        }

        private static List<string> SplitString(string list)
        {
            if (string.IsNullOrEmpty(list))
            {
                return new List<string>();
            }

            return list.Split(' ').ToList();
        }

        private static string JoinString(List<string> classList)
        {
            return string.Join(" ", classList.ToArray());
        }
    }
}