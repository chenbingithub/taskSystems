using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Web.Controllers;

namespace HNCJ.Web.Areas.WorkFlow.Controllers
{
    public class WFTempController : AdminBaseController
    {
        //
        // GET: /WFStem/
        public IWFTempService WFTempService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取全部信息
        public ActionResult GetAllWFTemps()
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
            var pagedata = WFTempService.LoagPageData(param);
            var tmp = pagedata.Select(u => new { u.ID,u.TempName,u.Description,u.ActivityType,u.SubTime,u.Remark,u.DelFlag });
            int total = param.Total;
            var data = new { total = total, rows = tmp.ToList() };
            return Json(data);
        }
        #endregion


        public ActionResult Add()
        {
            return View(new WFTemp { DelFlag = true,SubTime=DateTime.Now});
        }


        public ActionResult Delete(string ids)
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
            if (WFTempService.DeleteList(idList))
            {
                return Json(new { status = 1, msg = "删除成功！" });
            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });
            }
        }

        public ActionResult Edit(int id)
        {
            return View("Add", WFTempService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(WFTemp WFTemp)
        {
            if (ModelState.IsValid)
            {
                if (WFTemp.ID == 0)
                {
                    if (WFTempService.Insert(WFTemp))
                    {
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    if (WFTempService.Update(WFTemp))
                    {
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

        #region 发起流程
        public ActionResult StartWF() {
            ViewData.Model = WFTempService.GetEntity(u => u.DelFlag == true).ToList();
            return View();
        }
        #endregion
    }
}
