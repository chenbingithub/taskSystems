using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HNCJ.Common.Cache;
using HNCJ.IBLL;
using HNCJ.Model;
using Spring.Context;
using Spring.Context.Support;

namespace HNCJ.Common
{

    public class DoSetManage
    {
        public static ISysLogService SysLogService { get; set; }
        public static IUserInfoService UserInfoService { get; set; }
        public static UserInfo UserLogin { get; set; }
        static DoSetManage()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            DoSetManage.UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;
            DoSetManage.SysLogService = ctx.GetObject("SysLogService") as ISysLogService;
        }

        public static void SetUserInfo(UserInfo userinfo)
        {
            DoSetManage.UserLogin = userinfo;
        }

        public static bool IsIsAuthorized(string permissionItemCode)
        {
            DoSetManage.UserLogin = UserInfoService.GetModel(UserLogin.ID);
            if (UserLogin.UserName.Equals("admin")) return true;
            var list = UserLogin.ActionItem.Where(u => u.ActionItemNo.Contains(permissionItemCode.Trim())).ToList();
            var allrolesactionitem = (from a in UserLogin.RoleInfo
                from r in a.ActionItem
                where r.ActionItemNo.Contains(permissionItemCode.Trim())
                select r).ToList();
            if (list.Count > 0 || allrolesactionitem.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
