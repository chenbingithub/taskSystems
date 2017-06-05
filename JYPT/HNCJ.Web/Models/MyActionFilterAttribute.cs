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

namespace HNCJ.Web.Models
{
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        public bool IsCheckUserLogin = true;
        public bool IsAdmin = true;
        public bool IsRoleAction = true;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheckUserLogin)
            {
                string cookie=Utils.GetCookie("userLoginId");
                if (string.IsNullOrEmpty(cookie))
                {
                    if (IsAdmin)
                    {
                        filterContext.HttpContext.Response.Redirect("/UserLogin/AdminLogin");
                        return;
                    }
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Login");
                    return;
                }
                string userGuid = cookie;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache<UserInfo>(userGuid);
                if (userInfo == null)
                {
                    if (IsAdmin)
                    {
                        filterContext.HttpContext.Response.Redirect("/UserLogin/AdminLogin");
                        return;
                    }
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Login");
                    return;
                }
                //滑动窗口机制
                Common.Cache.CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));
                //if (IsRoleAction)
                //{
                //    string url = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
                //    var action = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString().ToLower();
                //    var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower();
                //    var areaName = HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"] == null ? "" : HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString().ToLower();
                //    string URL = "";
                //    if (areaName == "")
                //    {
                //        URL = "/" + controllerName + "/" + action;

                //    }
                //    else {
                //        URL = "/" + areaName + "/" + controllerName + "/" + action;
                    
                //    }
                    
                    
                //    string httpMethod = HttpContext.Current.Request.HttpMethod.ToLower();
                //    IApplicationContext ctx = ContextRegistry.GetContext();
                //    IActionInfoService ActionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                //    IRoleInfoService RoleInfoService = ctx.GetObject("RoleInfoService") as IRoleInfoService;
                //    IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
                //    var actionInfo = ActionInfoService.GetModel(a => a.Url.ToLower() == URL);
                //    if (actionInfo == null)
                //    {
                //        var data = ActionInfoService.GetEntity(a => a.Url.Contains(controllerName) && a.IsMenu == true).FirstOrDefault();
                //        if (data == null)
                //        {
                //            actionInfo = ActionInfoService.Add(URL, httpMethod);

                //        }
                //        else {
                //            ActionInfoService.Add(URL, httpMethod, data);
                //        }
                //        //HttpContext.Current.Response.Redirect("/Error.html");
                //    }
                //    if (userInfo.UserName == "admin") return;
                 
                //    var user = UserInfoService.GetEntity(u => u.ID == userInfo.ID).FirstOrDefault();
                   
                //    var actions = from r in user.RoleInfo
                //                  from a in r.ActionInfo
                //                  select a;
                //    var temp = (from a in actions
                //                where a.ID == actionInfo.ID
                //                select a).Count();
                //    var temp1 = (from b in user.ActionInfo
                //                 where b.ID == actionInfo.ID
                //                 select b).Count();
                //    if (temp <= 0 && temp1<=0)
                //    {
                //        //HttpContext.Current.Response.Redirect("/Error.html");
                //        HttpContext.Current.Response.Redirect("Userlogin/error");
                //    }
                //}
            }

        }
    }
}