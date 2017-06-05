using HNCJ.IBLL;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HNCJ.Web.Models
{
    public class Commcs
    {
        public static IUserInfoService UserInfoService{get;set;}
        static Commcs(){
            IApplicationContext ctx = ContextRegistry.GetContext();
            Commcs.UserInfoService =ctx.GetObject("UserInfoService") as IUserInfoService;
        }
        public static string IDToName(int Id) { 
            return UserInfoService.GetModel(Id).UserName;
        }
    }
}