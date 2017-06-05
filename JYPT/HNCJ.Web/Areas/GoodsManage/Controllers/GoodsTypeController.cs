using HNCJ.IBLL;
using HNCJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HNCJ.Common;
using HNCJ.Web.Controllers;

namespace HNCJ.Web.Areas.GoodsManage.Controllers
{
    public class GoodsTypeController : AdminBaseController
    {
        //
        // GET: /GoodsManage/GoodsType/
        public IGoodsTypeService GoodsTypeService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取全部用户信息
        public ActionResult GetAllGoodsTypes()
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
            var pagedata = GoodsTypeService.LoagPageData(param);
            var tmp = pagedata.ToList();
            var dd = tmp.Select(u => new { u.ID, u.TypeName, u.Url, NodeID = tmp.Where(r=>r.ID==u.NodeID).Select(t=>t.TypeName), u.RegTime, u.ModfiedOn, u.English });
            var data = new { total = param.Total, rows = dd };
            return Json(data);
        }
        #endregion

        #region 添加类型
        public ActionResult Add()
        {
            ViewBag.List = GoodsTypeService.GetList().OrderBy(u => u.ID);
            return View(new GoodsType { RegTime = DateTime.Now, ModfiedOn = DateTime.Now, DelFlag = true });
        }

        #endregion

        #region 删除类型
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (GoodsTypeService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除商品类型");
                return Json(new { status = 1, msg = "删除成功！" });

            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });

            }
        }
        #endregion

        #region 修改类型
        public ActionResult Edit(int id)
        {
            ViewBag.List = GoodsTypeService.GetList().OrderBy(u=>u.ID);
            return View("Add", GoodsTypeService.GetModel(u => u.ID == id));
        }
        [HttpPost]
        public ActionResult Save(GoodsType GoodsType)
        {
            if (ModelState.IsValid)
            {
                if (GoodsType.ID == 0)
                {
                    if (GoodsTypeService.Insert(GoodsType))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加商品类型");
                        return Json(new { status = 1, msg = "添加类型成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    GoodsType.ModfiedOn = DateTime.Now;
                    if (GoodsTypeService.Update(GoodsType))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "编辑商品类型");
                        return Json(new { status = 1, msg = "修改类型成功" });
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
       
    }
}
