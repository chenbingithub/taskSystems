using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Common;
using HNCJ.Web.Controllers;

namespace HNCJ.Web.Areas.BaseInfo.Controllers
{
    public class LoginLogController : AdminBaseController
    {
        //
        // GET: /BaseInfo/LoginLog/
        public ILoginLogService LoginLogService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllLoginLogs()
        {
            int pageSize = int.Parse(Request["rows"] ?? "20");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            var list = LoginLogService.GetEntity(l => l.DelFlag == true).ToList();


            var tmp = list.OrderByDescending(r => r.LoginTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(u => new { u.ID, u.UserName, u.UserInfoID, u.LoginTime, u.IP });
            int total = list.Count();
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data);
        }




        public ActionResult Add()
        {
            return View(new LoginLog { DelFlag = true,LoginTime=DateTime.Now });
        }


        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (LoginLogService.DeleteList(idList))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "删除登录日志");
                        return Json(new { status = 1, msg = "删除成功！" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
        }

        public ActionResult Edit(int id)
        {
            return View("Add", LoginLogService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(LoginLog LoginLog)
        {
            if (ModelState.IsValid)
            {
                if (LoginLog.ID == 0)
                {
                    if (LoginLogService.Insert(LoginLog))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加登录日志");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (LoginLogService.Update(LoginLog))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改登录日志");
                        return Json(new { status = 1, msg = "修改成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
            }
            else
            {
                return Json(new { status = 0, msg = "请输入合法的字段" });
            }
        }
    }
}
