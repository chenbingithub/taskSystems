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
    public class SysLogInfoController : AdminBaseController
    {
        //
        // GET: /BaseInfo/SysLogInfo/
        public ISysLogService SysLogService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
      

        
        public ActionResult GetAllSysLogs()
        {
            int pageSize = int.Parse(Request["rows"] ?? "20");
            int pageIndex = int.Parse(Request["page"] ?? "1");

            int itemid = int.Parse(Request["Itemid"] ?? "0");
            string keyString = Request["KeyString"];
            BaseParam param = new BaseParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
                Itemid = itemid,
                KeyString = keyString
            };
            var pagedata = SysLogService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID, u.UserName, u.UserInfoID, u.Operation,u.OperationTime });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data);
        }

        

       
        public ActionResult Add()
        {
            return View(new SysLog { DelFlag = true,OperationTime=DateTime.Now });
        }

        
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (SysLogService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除系统日志");
                return Json(new { status = 1, msg = "删除成功！" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }
       
        public ActionResult Edit(int id)
        {
            return View("Add", SysLogService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(SysLog SysLog)
        {
            if (ModelState.IsValid)
            {
                if (SysLog.ID == 0)
                {
                    if (SysLogService.Insert(SysLog))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加系统日志");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (SysLogService.Update(SysLog))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改系统日志");
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
