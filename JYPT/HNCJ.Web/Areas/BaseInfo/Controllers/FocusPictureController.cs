using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Common;

namespace HNCJ.Web.Areas.BaseInfo.Controllers
{
    public class FocusPictureController : AdminBaseController
    {
        //
        // GET: /FocusPicture/
        public IFocusPictureService FocusPictureService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取全部轮播图信息
        public ActionResult GetAllFocusPictures()
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
            var pagedata = FocusPictureService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID,u.Title,u.ImagePath,u.Url,u.RegTime,u.ModfiedOn });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加轮播图
        public ActionResult Add()
        {
            return View(new FocusPicture { RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true });
        }

        #endregion

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (FocusPictureService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除广告图");
                return Json(new { status = 1, msg = "删除成功！" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }

        public ActionResult Edit(int id)
        {
            return View("Add", FocusPictureService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(FocusPicture FocusPicture)
        {
            if (ModelState.IsValid)
            {
                if (FocusPicture.ID == 0)
                {
                    if (FocusPictureService.Insert(FocusPicture))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加广告图");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (FocusPictureService.Update(FocusPicture))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改广告图");
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
