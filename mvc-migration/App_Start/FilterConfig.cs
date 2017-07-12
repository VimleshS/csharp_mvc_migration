using System.Web;
using System.Web.Mvc;

namespace mvc_migration
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //enables authorize attribute on application level.
            filters.Add(new AuthorizeAttribute());
        }
    }
}
