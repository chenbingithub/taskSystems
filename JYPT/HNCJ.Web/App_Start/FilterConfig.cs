using HNCJ.Web.Models;
using System.Web;
using System.Web.Mvc;

namespace HNCJ.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionFilterAttibut());
            filters.Add(new MyActionFilterAttribute());
        }
    }
}