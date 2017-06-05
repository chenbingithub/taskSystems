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
    public class RoleInfoController : AdminBaseController
    {
        //
        // GET: /RoleInfo/
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IActionItemService ActionItemService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 获取全部信息
        public ActionResult GetAllRoleInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
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
            var pagedata = RoleInfoService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID, u.RoleName, u.RoleNo, u.State, u.Description, u.ModfiedOn, u.RegTime });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加角色
        public ActionResult Add()
        {
            return View(new RoleInfo { RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true, State = true });
        }

        #endregion

        #region 删除角色
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (RoleInfoService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除角色");
                return Json(new { status = 1, msg = "删除成功！" });

            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });

            }
        }
        #endregion

        #region 修改角色
        public ActionResult Edit(int id)
        {
            return View("Add", RoleInfoService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(RoleInfo roleInfo)
        {
            if (ModelState.IsValid)
            {
                if (roleInfo.ID == 0)
                {
                    if (RoleInfoService.Insert(roleInfo))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加角色");
                        return Json(new { status = 1, msg = "添加角色成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    roleInfo.ModfiedOn = DateTime.Now;
                    if (RoleInfoService.Update(roleInfo))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改角色");
                        return Json(new { status = 1, msg = "修改角色成功" });
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

        public ActionResult SetStatus(int roleid, bool state)
        {
            var entity = RoleInfoService.GetModel(u => u.ID == roleid);
            if (entity != null)
            {
                entity.State = !state;
                if (RoleInfoService.Update(entity))
                {
                    DoSetManage.SysLogService.Insert(LoginUser, "修改角色的状态");
                    return Json(new { status = 1, msg = "成功" });
                }
                else
                {
                    return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                }
            }
            else
            {
                return Json(new { status = 0, msg = "该角色不存在" });

            }

        }



        #region 关联模块
        public ActionResult SetPermission()
        {
            return View();
        }
        public ActionResult GetPermissionList(int roleid)
        {
            var actionids = RoleInfoService.GetModel(u => u.ID == roleid).ActionInfo.Select(r => r.ID);
            return Json(ActionInfoService.GetEntity(u => u.DelFlag == true).OrderBy(u => u.Sort).Select(u => new { u.ID, u.ActionName, ParentId = u.ParentId, @checked = actionids.Contains(u.ID) }));
        }
        public ActionResult ProcessSetPermission(int roleid, string actionids)
        {
            List<int> setactionIdList = new List<int>();
            if (!string.IsNullOrEmpty(actionids))
            {
                setactionIdList.AddRange(actionids.Split(',').Select(int.Parse));
            }
            if (RoleInfoService.SetPermission(roleid, setactionIdList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "设置菜单权限");
                return Json(new { status = 1, msg = "成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }

        }
        #endregion

        #region 关联模块
        public ActionResult SetPermissionItem()
        {
            return View();
        }
        public ActionResult GetPermissionItemList(int roleid)
        {
            var actionItemids = RoleInfoService.GetModel(u => u.ID == roleid).ActionItem.Select(r => r.ID);
            var actionids = RoleInfoService.GetModel(u => u.ID == roleid).ActionItem.Select(r => r.ActionInfoID);
           var allaction= ActionInfoService.GetList()
                .OrderBy(u => u.Sort).ToList()
                .Select(u => new { u.ID, ActionId = u.ID + "s", u.ActionName, ParentId = u.ParentId + "s", IsMenu = true, nocheck = u.ActionItem.Count < 1, @checked = actionids.Contains(u.ID) }).ToList();
            var allactionItem=ActionItemService.GetList().OrderBy(u => u.Sort).ToList()
                .Select(u => new { u.ID, ActionId = u.ID + "a", ActionName = u.ActionItemName, ParentId = u.ActionInfoID + "s", IsMenu = false, nocheck = false, @checked = actionItemids.Contains(u.ID) });
            allaction.AddRange(allactionItem);
            return Json(allaction);
        }
        public ActionResult ProcessSetPermissionItem(int roleid, string actionids)
        {
            List<int> setactionIdList = new List<int>();
            if (!string.IsNullOrEmpty(actionids))
            {
                setactionIdList.AddRange(actionids.Split(',').Select(int.Parse));
            }
            if (RoleInfoService.SetPermissionItem(roleid, setactionIdList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "设置操作权限");
                return Json(new { status = 1, msg = "成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }

        }
        #endregion
    }
}
