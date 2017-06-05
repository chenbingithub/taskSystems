using HNCJ.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Common;
using HNCJ.Model;
using HNCJ.Web.Controllers;

namespace HNCJ.Web.Areas.BaseInfo.Controllers
{
    public class PermissionItemController : AdminBaseController
    {
        //
        // GET: /BaseInfo/PermissionItem/
        // GET: /BaseInfo/Permission/
        public IActionItemService ActionItemService { get; set; }
        public ActionResult Index()
        {
            return View();
        }



        #region 添加
        public ActionResult Add(int actionId)
        {
            return View(new ActionItem { ActionInfoID = actionId, ID = 0, RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true });
        }

        #endregion

        #region 删除信息
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            var strIds = ids.Split(',');
            var idList = strIds.Select(int.Parse).ToList();
            if (ActionItemService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除操作权限");
                return Json( new { status = 1, msg = "删除成功！" } );
            }
            else
            {
                return Json( new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }
        #endregion

        #region 修改信息
        public ActionResult Edit(int id)
        {
            return View("Add", ActionItemService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(ActionItem ActionItem)
        {
            if (ModelState.IsValid)
            {
                if (ActionItem.ID == 0)
                {
                    if (ActionItemService.Insert(ActionItem))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加操作权限");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    ActionItem.ModfiedOn = DateTime.Now;
                    if (ActionItemService.Update(ActionItem))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改操作权限");
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
        #endregion

        public ActionResult GetList(int actionInfoID=0,int page=1,int rows=10)
        {
            var list=ActionItemService.GetEntity(p => p.ActionInfoID == actionInfoID).OrderBy(r => r.Sort).ToList();
            var data = list.Skip(rows*(page - 1)).Take(rows).Select(u=>new{u.ID,u.ActionItemName,u.ActionItemNo,u.Sort,u.ActionInfoID});
            return Json(new { total = list.Count, rows = data.ToList() });
        }
    }
}
