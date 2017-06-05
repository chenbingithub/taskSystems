using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Common;
using HNCJ.Web.Controllers;

namespace HNCJ.Web.Areas.BaseInfo.Controllers
{
    public class PermissionController : AdminBaseController
    {
        //
        // GET: /BaseInfo/Permission/
        public IActionInfoService ActionInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

    

        #region 添加
        public ActionResult Add()
        {
            return View(new ActionInfo { IsMenu = false, RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true });
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
            if (ActionInfoService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除菜单模块");
                return Json(new { status = 1, msg = "删除成功！" } );

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
            return View("Add", ActionInfoService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(ActionInfo actionInfo)
        {
            if (ModelState.IsValid)
            {
                if (actionInfo.ID == 0)
                {
                    if (ActionInfoService.Insert(actionInfo))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加菜单模块");
                        return Json(new { status = 1, msg = "添加权限成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    actionInfo.ModfiedOn = DateTime.Now;
                    if (ActionInfoService.Update(actionInfo))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改菜单模块");
                        return Json(new { status = 1, msg = "修改权限成功" });
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

        #region 上传图片
        public ActionResult Upload()
        {
            HttpPostedFileBase postFile = Request.Files["Filedata"];//接收文件

            if (postFile != null)
            {
                string fileName = Path.GetFileName(postFile.FileName); //文件名称
                string fileExt = Path.GetExtension(fileName); //文件的扩展名
                string dir = "/Content/Menu/";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir))); //创建文件夹
                string newfileName = Guid.NewGuid().ToString(); //新的文件名
                string fileDir = dir + newfileName + fileExt; //完整的路径
                postFile.SaveAs(Request.MapPath(fileDir)); //保存文件

                return Json(new {path = fileDir,status=1});
            }
            else
            {
                return Json(new { path = "", status =0 });
            }
        }
        #endregion

        public ActionResult SetIsMenu(int actionid, bool Ismenu)
        {
            var entity = ActionInfoService.GetModel(u => u.ID == actionid);
            if (entity != null)
            {
                entity.IsMenu = !Ismenu;
                if (ActionInfoService.Update(entity))
                {
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

        public ActionResult SeleteAction()
        {
            return View();
        }
        public ActionResult GetList()
        {
            return Json(ActionInfoService.GetEntity(u => u.DelFlag == true).OrderBy(r => r.Sort).Select(u => new { u.ID, u.ActionName, u.ParentId }));
        }
      

        public ActionResult GetActionList()
        {
            return Json(ActionInfoService.GetEntity(u => u.DelFlag == true).OrderBy(r => r.Sort).Select(u => new { u.ID, u.ActionName, u.ParentId,u.IsMenu,u.ParentName }));
        
        }

        public ActionResult Details(int id)
        {
            return View(ActionInfoService.GetModel(u => u.ID == id));
        }

    }
}
