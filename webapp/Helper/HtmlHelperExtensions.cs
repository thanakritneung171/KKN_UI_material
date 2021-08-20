using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KknWebApp
{
    public static class HtmlHelperExtensions
    {
        public static string isActive(this HtmlHelper html, string controller = null, string action = null)
        {
            string activeClass = "active";

            string actualAction = (string)html.ViewContext.RouteData.Values["action"];
            string actualController = (string)html.ViewContext.RouteData.Values["controller"];
            if (String.IsNullOrEmpty(controller)) controller = actualController;
            if (String.IsNullOrEmpty(action)) action = actualAction;
            return (controller == actualController && action == actualAction) ? activeClass : String.Empty;
        }
        public static RouteValueDictionary ConditionalDisable(bool isDetail, object htmlAttributes = null)
        {
            var dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (isDetail)
            {
                dictionary.Add("disabled", "disabled");
            }

            return dictionary;
        }

    }
}