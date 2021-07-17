#region Using

using System.Web.Mvc;

#endregion

namespace KKN_UI
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());
            //filters.Add(new KKN_UI.Filters.InitializeSimpleMembershipAttribute());
        }
    }
}