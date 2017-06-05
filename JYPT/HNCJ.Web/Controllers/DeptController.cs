using HNCJ.Common;
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
    public class DeptController : Controller
    {
        //
        // GET: /Dept/
        public IDeptService DeptService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IActionItemService ActionItemService { get; set; }

        #region 主界面
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 树结构列表
        public ActionResult DeptList()
        {
            return Json(DeptService.GetList().Select(u => new { u.ID,u.deptname,u.ParentId}));
        } 
        #endregion

        #region 添加或修改部门界面
        public ActionResult Add(int parentid = 0, string name = "")
        {
            return View(new Dept { ID = 0, ParentId = parentid, ParentName = name, RegTime = DateTime.Now, ModfiedOn = DateTime.Now });
        }
        public ActionResult Edit(int id)
        {
            return View("Add", DeptService.GetModel(u => u.ID == id));
        }
        public ActionResult Save(Dept Dept)
        {
            if (ModelState.IsValid) {
                if (Dept.ID == 0)
                {
                    if (DeptService.Insert(Dept))
                    {
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                   
                }
                else
                {
                    if (DeptService.Update(Dept))
                    {
                        return Json(new { status = 1, msg = "修改成功" });
                    }
                    else {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
            } else {
                return Json(new { status = 0, msg = "请输入合法的数据" });
            }
            
        }
        #endregion

        #region 删除部门
        public ActionResult Delete(int id)
        {
            try {
                var list = DeptService.GetModel(d => d.ID == id).UserInfo;
                if (list != null)
                {
                    return Json(new { status = 0, msg = "部门下有员工不能删除" });
                }
            }catch{
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
           
            if (DeptService.Delete(id))
            {
                return Json(new { status = 1, msg = "删除成功" });

            }
            else {
                return Json(new { status = 0, msg = "删除失败" });
            }
        }
        #endregion

        public ActionResult SetStatus(int userid, bool state)
        {
            var entity=UserInfoService.GetModel(u => u.ID == userid);
            if (entity != null)
            {
                entity.State = !state;
                if (UserInfoService.Update(entity))
                {
                    return Json(new { status = 1, msg = "成功" });
                }
                else
                {
                    return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                }
            }
            else {
                return Json(new { status = 0, msg = "该用户不存在" });
            
            }

        }

        #region 用户增删改查
        public ActionResult GetUserInfoListByDeptId(int deptId = 0)
        {
            int Size = int.Parse(Request["rows"] ?? "10");
            int page = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var pagedata = UserInfoService.LoagPageData(page, Size, deptId, out total);
            List<UserInfo> list = new List<UserInfo>();
            foreach (var item in pagedata.Select(u => u.ID))
            {
                UserInfo userinfo = new UserInfo();
                var entity = UserInfoService.GetModel(u => u.ID == item);
                userinfo.ID = entity.ID;
                userinfo.UserName = entity.UserName;
                entity.NickName = entity.NickName;
                userinfo.State = entity.State;
                userinfo.DeptName = entity.Dept.deptname;
                userinfo.RoleName = string.Join(",", entity.RoleInfo.Select(r => r.RoleName));
                userinfo.Telphone = "";
                list.Add(userinfo);
            }

            return Json(new { total = total, rows = list });
        }

        public ActionResult CreateUser(int deptId)
        {
            var entity = DeptService.GetModel(u => u.ID == deptId);
            if (entity != null)
            {
                return View(new UserInfo()
                {
                    DeptID = entity.ID,
                    DeptName = entity.deptname
                });
            }
            else
            {
                return View(new UserInfo());
            }

        }

        public ActionResult UpdateUser(int id)
        {
            var entity = UserInfoService.GetModel(u => u.ID == id);
            if (entity != null)
            {
                return View("CreateUser", entity);
            }
            else
            {
                return View("CreateUser", new UserInfo());
            }

        }
        public ActionResult SaveUser(UserInfo entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.ID == 0)
                {
                    if (UserInfoService.Exits(entity.UserName))
                    {
                        return Json(new { status = 0, msg = "用户名已存在" });
                    }
                    entity.RegTime = DateTime.Now;
                    entity.ModfiedOn = DateTime.Now;
                    entity.DelFlag = true;
                    entity.Avatar = "/Content/templates/main/images/user-avatar.png";
                    entity.salt = Utils.GetCheckCode(6); //获得6位的salt加密字符串
                    entity.UserPwd = DESEncrypt.Encrypt("123456", entity.salt);
                    if (UserInfoService.Insert(entity))
                    {
                        return Json(new { status = 2, msg = "添加用户成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    entity.ModfiedOn = DateTime.Now;
                    if (UserInfoService.Update(entity))
                    {
                        return Json(new { status = 2, msg = "修改用户成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }

                }
            }
            else
            {
                return Json(new { status = 0, msg = "请输入合法的数据" });
            }

        }

        public ActionResult DeleteUser(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var id in strIds)
            {
                idList.Add(int.Parse(id));
            }
            if (UserInfoService.DeleteList(idList))
            {
                return Json(new { status = 1, msg = "成功！" });

            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });

            }
        }
        /// <summary>
        /// 选择部门
        /// </summary>
        /// <returns></returns>
        public ActionResult SeleteDept()
        {
            return View();
        } 
        #endregion

        #region 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult SetPassword(int userid)
        {
            if (UserInfoService.SetPassword(userid))
            {
                return Json(new { status = 2, msg = "密码重置成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        } 
        #endregion

        #region 设置角色
        public ActionResult SetRole()
        {
            return View();
        }
        public ActionResult GetRoleList(int userid)
        {
            var roleids = UserInfoService.GetModel(u => u.ID == userid).RoleInfo.Select(r => r.ID);
            return Json(RoleInfoService.GetEntity(u => u.DelFlag == true).Select(u => new { u.ID, u.RoleName, ParentId = 0, @checked = roleids.Contains(u.ID) }));
        }
        public ActionResult ProcessSetRole(int userid, string roleids)
        {
            List<int> setRoleIdList = new List<int>();
            if (!string.IsNullOrEmpty(roleids))
            {
                foreach (var key in roleids.Split(','))
                {
                    setRoleIdList.Add(int.Parse(key));
                }
            }
            if (UserInfoService.SetRole(userid, setRoleIdList))
            {
                return Json(new { status = 1, msg = "成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }

        }  
        #endregion

        #region 设置权限
        public ActionResult SetPermission()
        {
            return View();
        }
        public ActionResult GetPermissionList(int userid)
        {
            var actionids = UserInfoService.GetModel(u => u.ID == userid).ActionInfo.Select(r => r.ID);
            return Json(ActionInfoService.GetEntity(u => u.DelFlag == true).OrderBy(u => u.Sort).Select(u => new { u.ID, u.ActionName, ParentId =u.ParentId, @checked = actionids.Contains(u.ID) }));
        }
        public ActionResult ProcessSetPermission(int userid, string actionids)
        {
            List<int> setactionIdList = new List<int>();
            if (!string.IsNullOrEmpty(actionids))
            {
                setactionIdList.AddRange(actionids.Split(',').Select(int.Parse));
            }
            if (UserInfoService.SetPermission(userid, setactionIdList))
            {
                return Json(new { status = 1, msg = "成功" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }
        public ActionResult SetPermissionItem()
        {
            return View();
        }
        public ActionResult GetPermissionItemList(int userid)
        {
            var actionItemids = UserInfoService.GetModel(u => u.ID == userid).ActionItem.Select(r => r.ID);
            var actionids = UserInfoService.GetModel(u => u.ID == userid).ActionItem.Select(r => r.ActionInfoID);
            var allaction = ActionInfoService.GetList()
                 .OrderBy(u => u.Sort).ToList()
                 .Select(u => new { u.ID, ActionId = u.ID + "s", u.ActionName, ParentId = u.ParentId + "s", IsMenu = true, nocheck = u.ActionItem.Count < 1, @checked = actionids.Contains(u.ID) }).ToList();
            var allactionItem = ActionItemService.GetList().OrderBy(u => u.Sort).ToList()
                .Select(u => new { u.ID, ActionId = u.ID + "a", ActionName = u.ActionItemName, ParentId = u.ActionInfoID + "s", IsMenu = false, nocheck = false, @checked = actionItemids.Contains(u.ID) });
            allaction.AddRange(allactionItem);
            return Json(allaction);
        }
        public ActionResult ProcessSetPermissionItem(int userid, string actionids)
        {
            List<int> setactionIdList = new List<int>();
            if (!string.IsNullOrEmpty(actionids))
            {
                setactionIdList.AddRange(actionids.Split(',').Select(int.Parse));
            }
            if (UserInfoService.SetPermissionItem(userid, setactionIdList))
            {
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
