using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HNCJ.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        
        public IGoodsTypeService GoodsTypeService { get; set; }
        //
        // GET: /Home/
        //[BaseController()]
        public ActionResult Index()
        {
            
            //ViewData.Model = this.LoginUser;
            return View();
        }
        #region 加载菜单图标
        public List<ActionInfo> LoadUserMenu()
        {
            int userId = this.LoginUser.ID;
            var user = UserInfoService.GetEntity(u => u.ID == userId && u.DelFlag == true).FirstOrDefault();
            var allRole = user.RoleInfo;
            var allRoleActionIds = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.ID).ToList();
            
            //把当前用户所有权限拿到
            allRoleActionIds.AddRange(user.ActionInfo.Select(a => a.ID));
            allRoleActionIds = allRoleActionIds.Distinct().ToList();//去重复值
            var actionList = ActionInfoService.GetEntity(a => allRoleActionIds.Contains(a.ID) && a.IsMenu == true && a.DelFlag == true).ToList();
            if (LoginUser.UserName == "admin") {
                actionList=ActionInfoService.GetEntity(a =>a.IsMenu == true && a.DelFlag == true).ToList();
            }
            return actionList.OrderBy(u=>u.Sort).ToList();
        } 
        #endregion

        #region Window7界面
        public ActionResult Win()
        {
            ViewBag.AllMenu = LoadUserMenu();
            ViewData.Model = this.LoginUser;
            return View();
        } 
        #endregion

        public ActionResult ff() {
            return View();
        }
        public ActionResult all() {
            var data=GoodsTypeService.GetEntity(u => u.DelFlag == true).ToList();
            var tmp = data.Where(r=>r.NodeID==0).Select(u => new { u.ID, u.TypeName, u.Url, u.NodeID, u.RegTime, u.ModfiedOn, u.English, children =data.Where(s=>s.NodeID==u.ID).ToList()  }).ToList();
            return Json(new { total = tmp.Count, rows = tmp });
        }
        public ActionResult children(int Method)
        {
            var data = GoodsTypeService.GetEntity(u => u.DelFlag == true&&u.NodeID==Method).ToList();
            var tmp = data.Select(u => new { u.ID, u.TypeName, u.Url, u.NodeID, u.RegTime, u.ModfiedOn, u.English }).ToList();
            return Json(new {rows = tmp });
        }
    }
}
