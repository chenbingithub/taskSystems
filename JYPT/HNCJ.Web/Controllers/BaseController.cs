using HNCJ.BLL;
using HNCJ.Common;
using HNCJ.IBLL;
using HNCJ.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HNCJ.Web.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                string userGuid = Utils.GetCookie("userLoginId");
                LoginUser = Common.Cache.CacheHelper.GetCache<UserInfo>(userGuid);
                base.OnActionExecuting(filterContext);
        }
        

    }
}
