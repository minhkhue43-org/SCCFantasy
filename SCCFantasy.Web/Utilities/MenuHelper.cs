using Microsoft.AspNetCore.Mvc.Rendering;

namespace SCCFantasy.Web.Utilities
{
    public static class MenuHelper
    {
        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
            {
                cssClass = "active";
            }

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
            {
                controller = currentController;
            }

            if (string.IsNullOrEmpty(action))
            {
                action = currentAction;
            }

            return controller == currentController && action == currentAction ? cssClass : string.Empty;
        }
    }
}
