using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GiveCRM.Web.Views {
	public static class MenuHelper {

	public static bool IsCurrentPage(
		this HtmlHelper htmlHelper,
		string controllerName,
		string actionName = null)
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
		string actionName = null,
		string additionalClasses = "")
	{
		List<string> classList = additionalClasses.Split(' ').ToList(); 
		if (IsCurrentPage(htmlHelper, controllerName, actionName))
		{
			classList.Add("active");
		}
		
		if (classList.Count == 0) {
		 return new MvcHtmlString("");
		}

		return new MvcHtmlString("class=\"" + string.Join(" ", classList.ToArray()) + "\"");
	}
  }
}