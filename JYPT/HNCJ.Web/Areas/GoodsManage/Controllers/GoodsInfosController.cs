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
    public class GoodsInfosController : AdminBaseController
    {
        //
        // GET: /GoodsManage/GoodsInfo/
        public IDeptService DeptService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IGoodsInfoService GoodsInfoService { get; set; }
        public IGoodsTypeService GoodsTypeService { get; set; }
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetDeptAndUserList()
        {
            var data = DeptService.GetList().ToList().Select(u => new { u.ID, TreeID = u.ID.ToString() + "d", Name = u.deptname, ParentId = u.ParentId.ToString() + "d", IsUser = false }).ToList();
            var userlist = UserInfoService.GetList().ToList().Select(u => new { u.ID, TreeID = u.ID.ToString() + "u", Name = u.UserName, ParentId = u.DeptID.ToString() + "d", IsUser = true }).ToList();
            data.AddRange(userlist);
            return Json(data);
        }

        public ActionResult ShowGoods()
        {
            return View();
        }
        public ActionResult ShowDept(int id=0)
        {
            return View(DeptService.GetModel(u=>u.ID==id));
        }
        public ActionResult GetGoodsList(int userId=0)
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            string keyString = Request["KeyString"];
            var goodslist=GoodsInfoService.GetEntity(u => u.UserInfoID == userId);
            if (!string.IsNullOrEmpty(keyString)) {
                goodslist = goodslist.Where(g => g.GoodsName.Contains(keyString));
            }
            var tmp = goodslist.OrderByDescending(r => r.RegTime).Select(u => new { u.ID, u.GoodsName, GoodsTypeName = u.GoodsType.TypeName, u.GoodsNum, u.GoodsPrice, u.GoodsRemark, u.RegTime }).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return Json(new { total = goodslist.Count(), rows = tmp.ToList() });
        }

        
        #region 添加
        public ActionResult Add()
        {
            ViewBag.allGoodsType = GoodsTypeService.GetEntity(u => u.DelFlag == true).ToList();
            GoodsInfo entity = new GoodsInfo() { 
                RegTime = DateTime.Now,
                ModfiedOn = DateTime.Now,
                DelFlag = false,
                Status = (int)HNCJ.Model.Enum.GoodsInfoEnum.UpShelves,
                UserInfoID = this.LoginUser.ID
            };
            return View(entity);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(GoodsInfo entity)
        {

            if (ModelState.IsValid)
            {
                if (entity.ID == 0)
                {
                    entity.DelFlag = false;
                    entity.UserInfoID = LoginUser.ID;
                    if (GoodsInfoService.Insert(entity))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "添加商品");
                        var userinfo = UserInfoService.GetModel(u => u.ID == LoginUser.ID);
                        var jifen = userinfo.Point ?? 0;
                        jifen += 200;
                        userinfo.Point = jifen;
                        UserInfoService.Update(userinfo);
                        return Json(new { status = 1, msg = "添加成功" });
                    }
                    else
                    {
                        return Json(new { status = 0, msg = "系统忙，请稍后重试" });
                    }
                }
                else
                {
                    var goodsold = GoodsInfoService.GetModel(g => g.ID == entity.ID);
                    if (goodsold.DelFlag??false)
                    {
                        var price = entity.GoodsPrice ?? 0;
                        if (price > goodsold.GoodsPrice)
                        {
                            return Json(new { status = 0, msg = "已经审核通过，只能把价格降低" });
                        }
                    }
                    entity.ModfiedOn = DateTime.Now;
                    
                    if (GoodsInfoService.Update(entity))
                    {
                        DoSetManage.SysLogService.Insert(LoginUser, "编辑商品");
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

        #region 删除信息
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要删除的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
            if (GoodsInfoService.DeleteList(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "删除商品");
                return Json(new { status = 1, msg = "删除成功！" });

            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });

            }
        }
        #endregion
        
        #region 修改信息
        public ActionResult Edit(int id=0)
        {
            ViewBag.allGoodsType = GoodsTypeService.GetEntity(u => u.DelFlag == true).ToList();
            return View("Add",GoodsInfoService.GetModel(u => u.ID == id));
        }
       
        #endregion

        #region 审核

        public ActionResult Review()
        {
            return View();
        }

        public ActionResult GetReviewList()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            string keyString = Request["KeyString"];
            var goodslist = GoodsInfoService.GetEntity(u => u.DelFlag == false);
            if (!string.IsNullOrEmpty(keyString))
            {
                goodslist = goodslist.Where(g => g.GoodsName.Contains(keyString));
            }
            var tmp = goodslist.OrderByDescending(r => r.RegTime).Select(u => new { u.ID, u.GoodsName, GoodsTypeName= u.GoodsType.TypeName, u.GoodsNum, u.GoodsPrice, u.GoodsRemark, u.RegTime }).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return Json(new { total = goodslist.Count(), rows = tmp.ToList() });
        }

        public ActionResult DoReview(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { status = 0, msg = "请选中要审核的数据！" });
            }
            string[] strIds = ids.Split(',');
            List<int> idList = strIds.Select(int.Parse).ToList();
         
            if (GoodsInfoService.DoReview(idList))
            {
                DoSetManage.SysLogService.Insert(LoginUser, "审核商品");
                return Json(new { status = 1, msg = "操作成功！" });

            }
            else
            {
                return Json(new { status = 0, msg = "系统忙，请稍后重试" });

            }

        }

        public ActionResult UpdatePrice(int goodsid, decimal price)
        {
            var goods=GoodsInfoService.GetModel(g => g.ID == goodsid);
            goods.GoodsPrice = price;
            GoodsInfoService.Update(goods);
            return Json(new{status=1,msg="操作成功"});
        }

        #endregion
    }
}
