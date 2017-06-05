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
    public class FreightController : AdminBaseController
    {
        //
        // GET: /Freight/
        public IFreightService FreightService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取全部信息
        public ActionResult GetAllFreights()
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
            var pagedata = FreightService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID,u.FreightName,u.FreightPrice,u.ModfiedOn, u.RegTime });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data);
        }
        #endregion

        #region 添加
        public ActionResult Add()
        {
            return View(new Freight { RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true });
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
            if (FreightService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除配送方式");
                return Json(new { status = 1, msg = "删除成功！" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }

        public ActionResult Edit(int id)
        {
            return View("Add", FreightService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(Freight Freight)
        {
            if (ModelState.IsValid)
            {
                if (Freight.ID == 0)
                {
                    if (FreightService.Insert(Freight))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加配送方式");
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (FreightService.Update(Freight))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "修改配送方式");
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
